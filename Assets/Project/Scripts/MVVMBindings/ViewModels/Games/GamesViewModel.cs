using System;
using System.Collections.Generic;
using System.Linq;
using UnityWeld.Binding;

[Binding]
public class GamesViewModel : BaseViewModel
{
    private List<GameItemData> gameItemDatas;
    private string orderTypeName;

    public OrderTypeEnum OrderType { get; set; } = OrderTypeEnum.Name;

    [Binding]
    public List<GameItemData> GameItemDatas
    {
        get => this.gameItemDatas;
        set => this.Set(ref this.gameItemDatas, value, nameof(this.GameItemDatas));
    }

    [Binding]
    public string OrderTypeName
    {
        get => this.orderTypeName;
        set => this.Set(ref this.orderTypeName, value, nameof(this.OrderTypeName));
    }

    private void Start()
    {
        var list = new List<GameItemData>();

        #region Fill list
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
        #endregion

        this.GameItemDatas = list;

        this.RefreshGamesOrder();
        this.OrderTypeName = this.FormatOrderTypeName();
    }

    public void IncrementOrderType()
    {
        var length = Enum.GetValues(typeof(OrderTypeEnum)).Length;

        int index = (int)this.OrderType;
        index++;
        if (index >= length)
        {
            index = 0;
        }

        this.OrderType = (OrderTypeEnum)index;
        this.RefreshGamesOrder();
        this.OrderTypeName = this.FormatOrderTypeName();
    }

    private void RefreshGamesOrder()
    {
        switch (this.OrderType)
        {
            case OrderTypeEnum.Name:
                this.GameItemDatas = this.GameItemDatas.OrderBy(w => w.Title).ToList();
                break;
            case OrderTypeEnum.ReverseName:
                this.GameItemDatas = this.GameItemDatas.OrderByDescending(w => w.Title).ToList();
                break;
            case OrderTypeEnum.Random:
                this.GameItemDatas.Shuffle();
                this.RaisePropertyChanged(nameof(this.GameItemDatas));
                break;
            default:
                break;
        }
    }

    private string FormatOrderTypeName()
    {
        string type = string.Empty;

        switch (this.OrderType)
        {
            case OrderTypeEnum.Name:
                type = "Name";
                break;
            case OrderTypeEnum.ReverseName:
                type = "Reverse name";
                break;
            case OrderTypeEnum.Random:
                type = "Random";
                break;
            default:
                type = "";
                break;
        }

        return $"Order by: {type}";
    }
}
