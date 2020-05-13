using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class CategoryPanelViewModel : MainViewModel
{
    private CategoryData categoryData;

    [SerializeField]
    private string title;

    [Binding]
    public string Title
    {
        get => this.title;
        set => this.Set(ref this.title, value, nameof(this.Title));
    }

    [Binding]
    public Color Color => App.MainColor;

    protected override void OnParametersChanged()
    {
        base.OnParametersChanged();

        if (this.Parameters != null && this.Parameters is CategoryData)
        {
            this.categoryData = (CategoryData)this.Parameters;
            this.RaiseAllPropertyChanged(typeof(GameItemPanelViewModel));

            this.UpdateDatas();
        }
    }

    private void UpdateDatas()
    {
        this.Title = this.categoryData?.Category ?? string.Empty;
    }
}
