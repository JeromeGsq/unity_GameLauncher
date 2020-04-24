using DG.Tweening;
using UnityEngine;

public static class MaterialExtension
{
    public static void SetBlurValue(this Material material, float endValue, float duration = 0.5f, float startValue = -1, Color color = default, Ease ease = Ease.InOutExpo)
    {
        material.SetFloat("_Radius", startValue != -1 ? startValue : material.GetFloat("_Radius"));
        // material.SetColor("_OverlayColor", color.Equals(default) ? Color.gray : color);

        DOTween.To(
            () => material.GetFloat("_Radius"),
            x => material.SetFloat("_Radius", x),
            endValue,
            duration
        ).SetEase(ease: ease);

        DOTween.To(
            () => material.GetColor("_OverlayColor"),
            x => material.SetColor("_OverlayColor", x),
            color,
            duration
        ).SetEase(ease: ease);
    }
}
