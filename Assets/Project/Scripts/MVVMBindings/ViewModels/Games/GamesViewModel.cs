using System;
using System.Collections.Generic;
using UnityWeld.Binding;

[Binding]
public class GamesViewModel : BaseViewModel
{
    private const string itemsJsonPath = "games.json";

    private List<CategoryData> categories;
    private string orderTypeName;

    public OrderTypeEnum OrderType { get; set; } = OrderTypeEnum.Name;

    [Binding]
    public List<CategoryData> Categories
    {
        get => this.categories;
        set => this.Set(ref this.categories, value, nameof(this.Categories));
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
            StreamingAssetsLoader.LoadJson<CategoryData[]>(
                itemsJsonPath,
                (list) =>
                {
                    this.Categories = new List<CategoryData>(list);
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
        /*
        switch (this.OrderType)
        {
            case OrderTypeEnum.Name:
                this.Categories = this.Categories.OrderBy(w => w.Title).ToList();
                break;
            case OrderTypeEnum.ReverseName:
                this.Categories = this.Categories.OrderByDescending(w => w.Title).ToList();
                break;
            case OrderTypeEnum.Random:
                this.Categories.Shuffle();
                this.RaisePropertyChanged(nameof(this.Categories));
                break;
            default:
                break;
        }
        */
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

        return $"Order by : {type}";
    }
}
