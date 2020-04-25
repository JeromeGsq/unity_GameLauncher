using DG.Tweening;
using InControl;
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
    }

    public void ShowAppsView()
    {
        this.gamesView.gameObject.SetActive(false);
        this.appsView.gameObject.SetActive(true);
        this.settingsView.gameObject.SetActive(false);
    }

    public void ShowSettingsView()
    {
        this.gamesView.gameObject.SetActive(false);
        this.appsView.gameObject.SetActive(false);
        this.settingsView.gameObject.SetActive(true);
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
}
