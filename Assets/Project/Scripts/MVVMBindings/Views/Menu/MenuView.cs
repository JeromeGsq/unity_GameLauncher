using DG.Tweening;
using InControl;
using System.Diagnostics;
using Toastapp.MVVM;
using UnityEngine;

[RequireComponent(typeof(MenuViewModel))]
public class MenuView : BaseView<MenuViewModel>
{
    private bool hasFocus;

    private GamesView gamesView;
    private AppsView appsView;
    private SettingsView settingsView;

    [Space]

    [SerializeField]
    private DOTweenAnimation fadeAnimation;

    [Space]

    [SerializeField]
    private GameObject shutdownPanel;

    protected override void Awake()
    {
        base.Awake();

        // Init all tabs views
        this.gamesView = NavigationService.Get.ShowViewModel(typeof(GamesViewModel), root: this.transform, hidePreviousView: false).GetComponent<GamesView>();
        this.appsView = NavigationService.Get.ShowViewModel(typeof(AppsViewModel), root: this.transform, hidePreviousView: false).GetComponent<AppsView>();
        this.settingsView = NavigationService.Get.ShowViewModel(typeof(SettingsViewModel), root: this.transform, hidePreviousView: false).GetComponent<SettingsView>();

        // Hide all tabs views
        this.gamesView.gameObject.SetActive(false);
        this.appsView.gameObject.SetActive(false);
        this.settingsView.gameObject.SetActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.CanvasGroup.alpha = 0;
        this.fadeAnimation?.tween?.PlayForward();
        this.hasFocus = true;
    }

    protected override void Update()
    {
        base.Update();
        if (!this.hasFocus)
        {
            return;
        }

        var gamepadState = InputManager.ActiveDevice;

        if (gamepadState.StartOrSelect.WasPressed)
        {
            this.Lock();
        }
    }

    public void ShowGamesView()
    {
        this.gamesView.gameObject.SetActive(true);
        this.appsView.gameObject.SetActive(false);
        this.settingsView.gameObject.SetActive(false);
        this.shutdownPanel.SetActive(false);

        this.gamesView.GainFocus();
    }

    public void ShowAppsView()
    {
        this.gamesView.gameObject.SetActive(false);
        this.appsView.gameObject.SetActive(true);
        this.settingsView.gameObject.SetActive(false);
        this.shutdownPanel.SetActive(false);

        this.appsView.GainFocus();
    }

    public void ShowSettingsView()
    {
        this.gamesView.gameObject.SetActive(false);
        this.appsView.gameObject.SetActive(false);
        this.settingsView.gameObject.SetActive(true);
        this.shutdownPanel.SetActive(false);

        this.settingsView.GainFocus();
    }

    public void ShowShutdownPanel()
    {
        this.gamesView.LoseFocus();
        this.appsView.LoseFocus();
        this.settingsView.LoseFocus();

        this.shutdownPanel.SetActive(true);
    }

    public void Lock()
    {
        this.fadeAnimation?.tween?.PlayBackwards();
        this.hasFocus = false;
        this.fadeAnimation?.tween.OnRewind(() =>
        {
            NavigationService.Get.CloseViewModel(this.gamesView.ViewModel);
            NavigationService.Get.CloseViewModel(this.appsView.ViewModel);
            NavigationService.Get.CloseViewModel(this.settingsView.ViewModel);
            this.ViewModel.CloseViewModel();
        });
    }

    public void Shutdown()
    {
        Process.Start("shutdown", "/s /t 0");
    }

    public void Reboot()
    {
        Process.Start("reboot", "/r /t 0");
    }
}
