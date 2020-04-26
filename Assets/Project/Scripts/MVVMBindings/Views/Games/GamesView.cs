using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GamesViewModel))]
public class GamesView : TabsView<GamesViewModel>
{
    private List<GameObject> gameItemPanels = new List<GameObject>();

    [SerializeField]
    private GameObject gameItemPanelPrefab;

    [Space(20)]

    [SerializeField]
    private Transform root;

    protected override void Awake()
    {
        base.Awake();
        // Do not disturbe uiSelectableManager during reloading, wait RefreshGameItems();
        this.UISelectableManager.enabled = false;
    }

    public override void OnPropertyChanged(object sender, PropertyChangedEventArgs property)
    {
        base.OnPropertyChanged(sender, property);

        if (property.PropertyName.Equals(nameof(this.ViewModel.GameItemDatas)))
        {
            this.RefreshGameItems();
        }
    }

    private void RefreshGameItems()
    {
        if (this.ViewModel?.GameItemDatas == null)
        {
            return;
        }

        // Do not disturbe uiSelectableManager during reloading
        this.UISelectableManager.enabled = false;

        foreach (var gameItemPanel in this.gameItemPanels)
        {
            Destroy(gameItemPanel);
        }
        this.gameItemPanels.Clear();

        for (int i = 0; i < this.ViewModel.GameItemDatas.Count; i++)
        {
            var gameItemData = this.ViewModel.GameItemDatas[i];
            var gameItemGameObject = Instantiate(gameItemPanelPrefab, this.root);

            gameItemGameObject.GetComponent<GameItemPanelViewModel>().GameItemData = gameItemData;
            this.gameItemPanels.Add(gameItemGameObject);
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
}
