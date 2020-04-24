using DG.Tweening;
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

        this.blurPanelMaterial = this.blurPanel?.material;
        this.blurPanelMaterial.SetBlurValue(0, 0);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.SetAndStretchToParentSize(this.RectTransform, this.transform.parent.GetComponent<RectTransform>());
        this.hasFocus = true;
    }

    public override void OnPropertyChanged(object sender, PropertyChangedEventArgs property)
    {
        base.OnPropertyChanged(sender, property);

        if (property.PropertyName.Equals(nameof(this.ViewModel.IsInBackground)))
        {
            this.hasFocus = true;

            this.fadeAnimation?.tween?.PlayBackwards();
            this.scaleAnimation?.tween?.PlayBackwards();

            AudioManager.Instance.PlayOneShot(this.unlockReverseSound);

            this.blurPanelMaterial.SetBlurValue(1);

            this.DelaySeconds(() =>
            {
                foreach (var pSystem in particleSystems)
                {
                    pSystem.SetActive(true);
                }
            }, 1);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (!this.hasFocus)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            AudioManager.Instance.PlayMove();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.hasFocus = false;

            foreach (var pSystem in particleSystems)
            {
                pSystem.SetActive(false);
            }

            this.fadeAnimation?.tween?.PlayForward();
            this.scaleAnimation?.tween?.PlayForward();

            AudioManager.Instance.PlayOneShot(this.unlockSound);

            this.blurPanelMaterial.SetBlurValue(16, color: new Color(0.3f, 0.3f, 0.3f));

            NavigationService.Get.ShowViewModel(typeof(MenuViewModel), hidePreviousView: false);
        }
    }

    private void SetAndStretchToParentSize(RectTransform rect, RectTransform parentRect)
    {
        rect.anchorMax = new Vector2(1, 1);
        rect.pivot = new Vector2(0.5f, 0.5f); rect.pivot = new Vector2(0.5f, 0.5f);
        rect.sizeDelta = Vector2.zero; rect.sizeDelta = Vector2.zero;
    }
}