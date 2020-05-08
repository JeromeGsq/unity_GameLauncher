using InControl;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class UISelectableToggleManager : MonoBehaviour
{
    private List<UISelectableToggle> toggles;

    [SerializeField]
    private UISelectableToggle firstSelected;

    [SerializeField]
    private ScrollRect scrollRect;

    private void Awake()
    {
        this.toggles = new List<UISelectableToggle>(this.transform.GetComponentsInChildren<UISelectableToggle>());
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

        this.scrollRect.horizontalNormalizedPosition =
            Mathf.Lerp(
                this.scrollRect.horizontalNormalizedPosition,
                (float)(this.toggles.Single(w => w.isOn).transform.GetSiblingIndex()) / (float)((this.scrollRect.content.transform.childCount - 1)),
                Time.deltaTime * 10
            );

        for (int i = 0; i < this.toggles.Count; i++)
        {
            if (this.toggles[i].isOn)
            {
                if (gamepadState.R1.WasPressed)
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
                else if (gamepadState.L1.WasPressed)
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
}
