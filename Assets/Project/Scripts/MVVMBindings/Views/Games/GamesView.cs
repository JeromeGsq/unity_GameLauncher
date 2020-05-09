using InControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GamesViewModel))]
public class GamesView : TabsView<GamesViewModel>
{
    private List<GameObject> gameItemPanels = new List<GameObject>();
    private List<GameObject> categoryItemPanels = new List<GameObject>();

    [SerializeField]
    private UISelectableCategoriesManager uiSelectableCategoriesManager;

    [SerializeField]
    private GameObject gameItemPanelPrefab;

    [SerializeField]
    private GameObject categoryPrefab;

    [Space(20)]

    [SerializeField]
    private Transform gamesRoot;

    [SerializeField]
    private Transform categoriesRoot;

    [Space(20)]

    [SerializeField]
    private GameObject SlidePanel;

    protected override void Awake()
    {
        base.Awake();
        // Do not disturbe uiSelectableManager during reloading, wait RefreshGameItems();
        this.UISelectableManager.enabled = false;
        this.uiSelectableCategoriesManager.enabled = false;
    }

    public override void OnPropertyChanged(object sender, PropertyChangedEventArgs property)
    {
        base.OnPropertyChanged(sender, property);

        if (property.PropertyName.Equals(nameof(this.ViewModel.Categories)))
        {
            this.RefreshCategories();
        }
        else if (property.PropertyName.Equals(nameof(this.ViewModel.CategoryIndex)))
        {
            this.StartCoroutine(this.RefreshGameItems());
        }
    }

    private void RefreshCategories()
    {
        if (this.ViewModel?.Categories == null)
        {
            return;
        }

        // Do not disturbe uiSelectableManager during reloading
        this.uiSelectableCategoriesManager.enabled = false;

        // Clear list
        foreach (var categoryItemPanel in this.categoryItemPanels)
        {
            Destroy(categoryItemPanel);
        }
        this.categoryItemPanels.Clear();

        for (int i = 0; i < this.ViewModel.Categories.Count; i++)
        {
            var category = this.ViewModel.Categories[i];
            var categoryGameObject = Instantiate(this.categoryPrefab, this.categoriesRoot);
            categoryGameObject.GetComponent<CategoryPanelViewModel>().SetParameters(category);
            var uiSelectable = categoryGameObject.GetComponent<UISelectableToggle>();
            uiSelectable.group = this.uiSelectableCategoriesManager.Group;
            uiSelectable.OnFocusSelected.AddListener(() => { this.ViewModel.SelectCategory(category.Category); });
            this.categoryItemPanels.Add(categoryGameObject);
        }

        var firstSelectable = this.categoryItemPanels.FirstOrDefault();
        if (firstSelectable != null)
        {
            this.uiSelectableCategoriesManager.AssignDefaultSelectable(firstSelectable.GetComponent<UISelectableToggle>());
            this.uiSelectableCategoriesManager.RefreshTogglesList();
            // uiSelectableManager is now ready to assign its FocusedButton and handle events
            this.uiSelectableCategoriesManager.enabled = true;
        }
    }

    private IEnumerator RefreshGameItems()
    {
        if (this.ViewModel?.Categories[this.ViewModel.CategoryIndex] == null)
        {
            yield break;
        }

        // Do not disturbe uiSelectableManager during reloading
        this.UISelectableManager.enabled = false;

        // Clear list
        foreach (var gameItemPanel in this.gameItemPanels)
        {
            Destroy(gameItemPanel);
        }
        this.gameItemPanels.Clear();


        for (int i = 0; i < this.ViewModel.Categories[this.ViewModel.CategoryIndex].Items.Count; i++)
        {
            var gameItemData = this.ViewModel.Categories[this.ViewModel.CategoryIndex].Items[i];
            var gameItemGameObject = Instantiate(this.gameItemPanelPrefab, this.gamesRoot);
            gameItemGameObject.GetComponent<GameItemPanelViewModel>().SetParameters(gameItemData);
            this.gameItemPanels.Add(gameItemGameObject);
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < this.gameItemPanels.Count; i++)
        {
            var gameItem = this.gameItemPanels[i];
            var uiSelectable = gameItem.GetComponent<UISelectable>();

            if (i - 1 >= 0)
            {
                uiSelectable.Left = this.gameItemPanels[i - 1]?.GetComponent<Selectable>();
            }

            if (i + 1 < this.gameItemPanels.Count)
            {
                uiSelectable.Right = this.gameItemPanels[i + 1]?.GetComponent<Selectable>();
            }
        }

        var firstSelectable = this.gameItemPanels.FirstOrDefault();
        if (firstSelectable != null)
        {
            this.UISelectableManager.AssignDefaultSelectable(firstSelectable.GetComponent<UISelectable>());

            // uiSelectableManager is now ready to assign its FocusedButton and handle events
            this.UISelectableManager.enabled = true;
        }
    }

    public override void GainFocus()
    {
        base.GainFocus();
        this.SlidePanel.SetActive(true);
    }

    public override void LoseFocus()
    {
        base.LoseFocus();
        this.SlidePanel.SetActive(false);
    }
}