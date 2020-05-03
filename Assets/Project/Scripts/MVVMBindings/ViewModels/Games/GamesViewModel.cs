using System;
using System.Collections.Generic;
using System.Linq;
using UnityWeld.Binding;
using Newtonsoft.Json;

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
        this.StartCoroutine(
            StreamingAssetsLoader.LoadJson<GameItemData[]>(
                "games.json",
                (list) =>
                {
                    this.GameItemDatas = new List<GameItemData>(list);
                    this.OrderTypeName = this.FormatOrderTypeName();
                    this.RefreshGamesOrder();
                }
            )
        );
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
                type = "Title";
                break;
            case OrderTypeEnum.ReverseName:
                type = "Reverse title";
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
