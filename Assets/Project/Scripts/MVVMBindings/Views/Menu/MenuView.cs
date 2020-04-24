using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MenuViewModel))]
public class MenuView : BaseView<MenuViewModel>
{
    [SerializeField]
    private DOTweenAnimation fadeAnimation;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.CanvasGroup.alpha = 0;
        this.fadeAnimation?.tween?.PlayForward();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.fadeAnimation?.tween?.PlayBackwards();
            this.fadeAnimation?.tween.OnRewind(() =>
            {
                this.ViewModel.CloseViewModel();
            });
        }
    }
}
