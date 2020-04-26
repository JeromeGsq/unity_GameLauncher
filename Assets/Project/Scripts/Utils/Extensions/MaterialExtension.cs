using DG.Tweening;
using UnityEngine;

public static class MaterialExtension
{
    public static void SetBlurValue(this Material material, float endValue, float duration = 0.5f, float startValue = -1, Ease ease = Ease.InOutExpo)
    {
        material.SetFloat("_Radius", startValue != -1 ? startValue : material.GetFloat("_Radius"));

        DOTween.To(
            () => material.GetFloat("_Radius"),
            x => material.SetFloat("_Radius", x),
            endValue,
            duration
        ).SetEase(ease: ease);
    }
}
