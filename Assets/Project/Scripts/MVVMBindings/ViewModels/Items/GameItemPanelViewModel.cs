using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class GameItemPanelViewModel : MainViewModel
{
    private Sprite cover;
    private Sprite background;
    private Sprite icon;

    private GameItemData gameItemData;

    [Binding]
    public Sprite Cover
    {
        get => this.cover;
        set => this.Set(ref this.cover, value, nameof(this.Cover));
    }

    [Binding]
    public Sprite Background
    {
        get => this.background;
        set => this.Set(ref this.background, value, nameof(this.Background));
    }

    [Binding]
    public Sprite Icon
    {
        get => this.icon;
        set => this.Set(ref this.icon, value, nameof(this.Icon));
    }


    [Binding]
    public string Title => this.gameItemData?.Title ?? string.Empty;

    [Binding]
    public string ExecutableLink => this.gameItemData?.ExecutableLink ?? string.Empty;

    protected override void OnParametersChanged()
    {
        base.OnParametersChanged();

        if (this.Parameters != null && this.Parameters is GameItemData)
        {
            this.gameItemData = (GameItemData)this.Parameters;
        }

        this.UpdateImages();
    }

    [Binding]
    public void RunApp()
    {
        if (!string.IsNullOrEmpty(this.ExecutableLink) && this.ExecutableLink.Contains("steam://"))
        {
            var processInfo = new System.Diagnostics.ProcessStartInfo("explorer.exe", $"{this.ExecutableLink}");
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;

            var process = System.Diagnostics.Process.Start(processInfo);

            process.WaitForExit();
            process.Close();
        }
    }

    private void UpdateImages()
    {
        if (this.gameItemData == null)
        {
            Debug.LogWarning("GameItemPanelViewModel : UpdateImages() : gameItemData is null");
            return;
        }

        var coverPath = $"Images/Covers/{this.gameItemData?.FileName}.jpg";
        var backgroundPath = $"Images/Backgrounds/{this.gameItemData?.FileName}.jpg";
        var iconPath = $"Images/Icons/{this.gameItemData?.FileName}.png";

        this.StartCoroutine(
            StreamingAssetsLoader.LoadSprite(
                coverPath,
                (image) => this.Cover = image
            )
        );

        this.StartCoroutine(
            StreamingAssetsLoader.LoadSprite(
                backgroundPath,
                (image) => this.Background = image
            )
        );

        this.StartCoroutine(
            StreamingAssetsLoader.LoadSprite(
                iconPath,
                (image) => this.Icon = image
            )
        );
    }
}
