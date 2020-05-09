using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
using System.Linq;

[Binding]
public class GamesViewModel : BaseViewModel
{
    private const string itemsJsonPath = "games.json";

    private string orderTypeName;

    private List<CategoryData> categories;
    private int categoryIndex;

    [Binding]
    public List<CategoryData> Categories
    {
        get => this.categories;
        set => this.Set(ref this.categories, value, nameof(this.Categories));
    }

    [Binding]
    public int CategoryIndex
    {
        get => this.categoryIndex;
        set => this.Set(ref this.categoryIndex, value, nameof(this.CategoryIndex));
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
                }
            )
        );
    }

    public void SelectCategory(string category)
    {
        this.CategoryIndex = this.Categories.IndexOf(this.Categories.FirstOrDefault(item => item.Category.Equals(category)));
    }
}
