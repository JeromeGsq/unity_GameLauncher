using System.Collections.Generic;
using UnityWeld.Binding;

[Binding]
public class GamesViewModel : BaseViewModel
{
    private List<GameItemData> gameItemDatas;

    [Binding]
    public List<GameItemData> GameItemDatas
    {
        get => this.gameItemDatas;
        set => this.Set(ref this.gameItemDatas, value, nameof(this.GameItemDatas));
    }

    private void Start()
    {
        var list = new List<GameItemData>();
        list.Add(new GameItemData
        {
            Title = "Resident Evil 0: HD Remaster",
            FileName = "re0",
            ExecutableLink = "steam://rungameid/339340"
        });

        list.Add(new GameItemData
        {
            Title = "Resident Evil: HD Remaster",
            FileName = "re1",
        });

        list.Add(new GameItemData
        {
            Title = "Resident Evil 2 (2019)",
            FileName = "re2",
        });

        list.Add(new GameItemData
        {
            Title = "Resident Evil 3 (2020)",
            FileName = "re3",
        });

        this.GameItemDatas = list;
    }
}
