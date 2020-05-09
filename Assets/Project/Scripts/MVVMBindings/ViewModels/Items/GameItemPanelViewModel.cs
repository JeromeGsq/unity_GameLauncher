using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class GameItemPanelViewModel : MainViewModel
{
    [SerializeField]
    private Texture cover;
    [SerializeField]
    private Texture background;
    [SerializeField]
    private Texture icon;

    private GameItemData gameItemData;

    [Binding]
    public Texture Cover
    {
        get => this.cover;
        set => this.Set(ref this.cover, value, nameof(this.Cover));
    }

    [Binding]
    public Texture Background
    {
        get => this.background;
        set => this.Set(ref this.background, value, nameof(this.Background));
    }

    [Binding]
    public Texture Icon
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
            this.RaiseAllPropertyChanged(typeof(GameItemPanelViewModel));

            this.UpdateImages();
        }
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

            App.Instance.LaunchGame();
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
            StreamingAssetsLoader.LoadTexture2D(
                coverPath,
                (image) => this.Cover = image
            )
        );

        this.StartCoroutine(
            StreamingAssetsLoader.LoadTexture2D(
                backgroundPath,
                (image) => this.Background = image
            )
        );

        this.StartCoroutine(
            StreamingAssetsLoader.LoadTexture2D(
                iconPath,
                (image) => this.Icon = image
            )
        );
    }
}
