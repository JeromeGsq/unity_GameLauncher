using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
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

    [SerializeField]
    private ScrollRect scrollRect;

    protected override void Awake()
    {
        base.Awake();
        // Do not disturbe uiSelectableManager during reloading, wait RefreshGameItems();
        this.UISelectableManager.enabled = false;
    }

    protected override void Update()
    {
        base.Update();
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

            gameItemGameObject.GetComponent<GameItemPanelViewModel>().SetParameters(gameItemData);
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

public static class RendererExtensions
{
    /// <summary>
    /// Counts the bounding box corners of the given RectTransform that are visible from the given Camera in screen space.
    /// </summary>
    /// <returns>The amount of bounding box corners that are visible from the Camera.</returns>
    /// <param name="rectTransform">Rect transform.</param>
    /// <param name="camera">Camera.</param>
    private static int CountCornersVisibleFrom(this RectTransform rectTransform, Camera camera)
    {
        Rect screenBounds = new Rect(0f, 0f, Screen.width, Screen.height); // Screen space bounds (assumes camera renders across the entire screen)
        Vector3[] objectCorners = new Vector3[4];
        rectTransform.GetWorldCorners(objectCorners);

        int visibleCorners = 0;
        Vector3 tempScreenSpaceCorner; // Cached
        for (var i = 0; i < objectCorners.Length; i++) // For each corner in rectTransform
        {
            tempScreenSpaceCorner = camera.WorldToScreenPoint(objectCorners[i]); // Transform world space position of corner to screen space
            if (screenBounds.Contains(tempScreenSpaceCorner)) // If the corner is inside the screen
            {
                visibleCorners++;
            }
        }
        return visibleCorners;
    }

    /// <summary>
    /// Determines if this RectTransform is fully visible from the specified camera.
    /// Works by checking if each bounding box corner of this RectTransform is inside the cameras screen space view frustrum.
    /// </summary>
    /// <returns><c>true</c> if is fully visible from the specified camera; otherwise, <c>false</c>.</returns>
    /// <param name="rectTransform">Rect transform.</param>
    /// <param name="camera">Camera.</param>
    public static bool IsFullyVisibleFrom(this RectTransform rectTransform, Camera camera)
    {
        return CountCornersVisibleFrom(rectTransform, camera) == 4; // True if all 4 corners are visible
    }

    /// <summary>
    /// Determines if this RectTransform is at least partially visible from the specified camera.
    /// Works by checking if any bounding box corner of this RectTransform is inside the cameras screen space view frustrum.
    /// </summary>
    /// <returns><c>true</c> if is at least partially visible from the specified camera; otherwise, <c>false</c>.</returns>
    /// <param name="rectTransform">Rect transform.</param>
    /// <param name="camera">Camera.</param>
    public static bool IsVisibleFrom(this RectTransform rectTransform, Camera camera)
    {
        return CountCornersVisibleFrom(rectTransform, camera) > 0; // True if any corners are visible
    }
}