using InControl;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class UISelectableCategoriesManager : MonoBehaviour
{
    private List<UISelectableToggle> toggles;
    [SerializeField]
    private UISelectableToggle firstSelected;

    [SerializeField]
    private ScrollRect scrollRect;

    public ToggleGroup Group { get; set; }

    private void Awake()
    {
        this.Group = this.GetComponent<ToggleGroup>();
        this.RefreshTogglesList();
    }

    private void Start()
    {
        if (this.firstSelected != null)
        {
            this.firstSelected.isOn = true;
        }
    }

    private void Update()
    {
        var gamepadState = InputManager.ActiveDevice;

        var index = this.toggles?.FirstOrDefault(w => w?.isOn ?? false)?.transform?.GetSiblingIndex() ?? 0;
        this.scrollRect.verticalNormalizedPosition =
            Mathf.Lerp(
                this.scrollRect.verticalNormalizedPosition,
                (float)(index) / (float)((this.scrollRect.content.transform.childCount - 1)),
                Time.deltaTime * 10
            );

        for (int i = 0; i < this.toggles.Count; i++)
        {
            if (this.toggles[i].isOn)
            {
                if (gamepadState.DPadDown.WasPressed || gamepadState.LeftStick.Down.WasPressed)
                {
                    if (i + 1 >= this.toggles.Count)
                    {
                        this.toggles.Last().isOn = true;
                        break;
                    }
                    else
                    {
                        this.toggles[i + 1].isOn = true;
                        break;
                    }
                }
                else if (gamepadState.DPadUp.WasPressed || gamepadState.LeftStick.Up.WasPressed)
                {
                    if (i - 1 <= 0)
                    {
                        this.toggles[0].isOn = true;
                        break;
                    }
                    else
                    {
                        this.toggles[i - 1].isOn = true;
                        break;
                    }
                }
            }
        }
    }

    public void AssignDefaultSelectable(UISelectableToggle uiSelectable)
    {
        if (uiSelectable != null)
        {
            this.firstSelected = uiSelectable;
            this.firstSelected.isOn = true;
        }
    }

    public void RefreshTogglesList()
    {
        this.toggles = new List<UISelectableToggle>(this.transform.GetComponentsInChildren<UISelectableToggle>());
    }
}
