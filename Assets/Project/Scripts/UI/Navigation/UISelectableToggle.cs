#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class UISelectableToggle : Toggle
{
    [SerializeField]
    private ButtonClickedEvent onFocusSelected;

    [SerializeField]
    private ButtonClickedEvent onFocusDeselected;

    public ButtonClickedEvent OnFocusSelected { get => this.onFocusSelected; set => this.onFocusSelected = value; }
    public ButtonClickedEvent OnFocusDeselected { get => this.onFocusDeselected; set => this.onFocusDeselected = value; }

    protected override void Awake()
    {
        base.Awake();
        this.onFocusDeselected.Invoke();

        this.onValueChanged.AddListener(this.OnValueChanged);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        this.onValueChanged.RemoveAllListeners();
    }

    private void OnValueChanged(bool value)
    {
        if (value)
        {
            this.onFocusSelected.Invoke();
        }
        else
        {
            this.onFocusDeselected.Invoke();
        }
    }
}