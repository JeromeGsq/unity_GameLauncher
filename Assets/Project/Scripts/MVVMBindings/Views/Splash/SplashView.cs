using DG.Tweening;
using InControl;
using System.Collections.Generic;
using System.ComponentModel;
using Toastapp.MVVM;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SplashViewModel))]
public class SplashView : BaseView<SplashViewModel>
{
    private bool hasFocus;
    private Material blurPanelMaterial;

    [SerializeField]
    private DOTweenAnimation fadeAnimation;

    [SerializeField]
    private DOTweenAnimation scaleAnimation;

    [SerializeField]
    private DOTweenAnimation unlockAnimation;

    [Space]

    [SerializeField]
    private Image blurPanel;

    [Space]

    [SerializeField]
    private List<GameObject> particleSystems;

    [Space]

    [SerializeField]
    private AudioClip unlockSound;

    [SerializeField]
    private AudioClip unlockReverseSound;

    protected override void Awake()
    {
        base.Awake();
        this.CanvasGroup.alpha = 1;

        this.blurPanelMaterial = Instantiate(this.blurPanel.material);
        this.blurPanel.material = this.blurPanelMaterial;
        this.blurPanelMaterial.SetBlurValue(0, 0);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.hasFocus = true;
    }

    public override void OnPropertyChanged(object sender, PropertyChangedEventArgs property)
    {
        base.OnPropertyChanged(sender, property);

        if (property.PropertyName.Equals(nameof(this.ViewModel.IsInBackground)))
        {
            this.Resume();
        }
    }

    protected override void Update()
    {
        base.Update();
        if (!this.hasFocus)
        {
            return;
        }

        var gamepadState = InputManager.ActiveDevice;

        if (gamepadState.Cross.WasPressed || gamepadState.StartOrSelect.WasPressed)
        {
            this.Unlock();
        }
    }

    public void Unlock()
    {
        this.hasFocus = false;

        this.fadeAnimation?.tween?.PlayForward();
        this.scaleAnimation?.tween?.PlayForward();
        this.unlockAnimation?.tween?.PlayForward();

        this.blurPanelMaterial.SetBlurValue(16, color: new Color(0.3f, 0.3f, 0.3f));

        AudioManager.Instance.PlayOneShot(this.unlockSound);

        this.DelaySeconds(() =>
        {
            NavigationService.Get.ShowViewModel(typeof(MenuViewModel), hidePreviousView: false);
        }, 0.4f);

    }

    private void Resume()
    {
        this.hasFocus = true;

        this.fadeAnimation?.tween?.PlayBackwards();
        this.scaleAnimation?.tween?.PlayBackwards();
        this.unlockAnimation?.tween?.PlayBackwards();

        this.blurPanelMaterial.SetBlurValue(1);

        AudioManager.Instance.PlayOneShot(this.unlockReverseSound);
    }
}