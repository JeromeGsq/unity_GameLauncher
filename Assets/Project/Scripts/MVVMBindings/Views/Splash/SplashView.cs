using DG.Tweening;
using InControl;
using SuperBlur;
using System.ComponentModel;
using Toastapp.MVVM;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SplashViewModel))]
public class SplashView : BaseView<SplashViewModel>
{
    private bool hasFocus;

    [SerializeField]
    private DOTweenAnimation fadeAnimation;

    [SerializeField]
    private DOTweenAnimation scaleAnimation;

    [SerializeField]
    private DOTweenAnimation unlockAnimation;

    [Space]

    [SerializeField]
    private AudioClip unlockSound;

    [SerializeField]
    private AudioClip unlockReverseSound;

    protected override void Awake()
    {
        base.Awake();
        this.CanvasGroup.alpha = 1;

        SuperBlurBase.Instance.SetInterpolation(0);
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
        if (gamepadState.StartOrSelect.WasPressed)
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

        AudioManager.Instance.PlayOneShot(this.unlockSound);

        DOTween.To(() =>
           SuperBlurBase.Instance.interpolation,
            x => SuperBlurBase.Instance.SetInterpolation(x),
            1,
            0.4f
        ).OnComplete(() =>
            NavigationService.Get.ShowViewModel(typeof(MenuViewModel),
            hidePreviousView: false)
        );
    }

    private void Resume()
    {
        this.hasFocus = true;

        this.fadeAnimation?.tween?.PlayBackwards();
        this.scaleAnimation?.tween?.PlayBackwards();
        this.unlockAnimation?.tween?.PlayBackwards();

        DOTween.To(() =>
           SuperBlurBase.Instance.interpolation,
            x => SuperBlurBase.Instance.SetInterpolation(x),
            0,
            0.4f
        );

        AudioManager.Instance.PlayOneShot(this.unlockReverseSound);
    }
}