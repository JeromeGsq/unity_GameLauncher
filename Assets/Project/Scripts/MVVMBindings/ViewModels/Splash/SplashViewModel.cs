using UnityWeld.Binding;
using UnityEngine;
[Binding]
public class SplashViewModel : MainViewModel
{
    [SerializeField]
    private Texture wallpaper;

    [Binding]
    public Texture Wallpaper
    {
        get => this.wallpaper;
        set => this.Set(ref this.wallpaper, value, nameof(this.Wallpaper));
    }

    private void OnEnable()
    {
        var wallpaperPath = "wallpaper.jpg";
        this.StartCoroutine(
            StreamingAssetsLoader.LoadTexture2D(
                wallpaperPath,
                (image) =>
                {
                    this.Wallpaper = image;
                }
            )
        );
    }
}
