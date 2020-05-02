using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class UISelectable : Selectable, ISelectHandler
{
    private UISelectableManager buttonManager;
    private Selectable selectable;
    private bool hasFocus;

    [SerializeField]
    private ButtonClickedEvent onFocusSelected;

    [SerializeField]
    private ButtonClickedEvent onFocusDeselected;

    [SerializeField]
    private ButtonClickedEvent onClick;

    protected bool HasFocus => this.hasFocus;

    public Selectable Up { get; set; }
    public Selectable Down { get; set; }
    public Selectable Left { get; set; }
    public Selectable Right { get; set; }

    public ButtonClickedEvent OnFocusSelected { get => this.onFocusSelected; set => this.onFocusSelected = value; }
    public ButtonClickedEvent OnFocusDeselected { get => this.onFocusDeselected; set => this.onFocusDeselected = value; }
    public ButtonClickedEvent OnClick { get => this.onClick; set => this.onClick = value; }

    protected override void Awake()
    {
        base.Awake();
        this.selectable = this.GetComponent<Selectable>();
        this.buttonManager = this.GetComponentInParent<UISelectableManager>();
        this.OnFocusDeselected?.Invoke();

        if (this.selectable != null)
        {
            this.Up = this.selectable.navigation.selectOnUp;
            this.Down = this.selectable.navigation.selectOnDown;
            this.Left = this.selectable.navigation.selectOnLeft;
            this.Right = this.selectable.navigation.selectOnRight;
        }
        this.buttonManager.OnButtonFocusChanged += this.UpdateFocus;

        this.UpdateFocus(this.buttonManager?.FocusedButton);
    }

    private void UpdateFocus(Selectable buttonFocus)
    {
        this.hasFocus = buttonFocus == this;
        if (this.hasFocus)
        {
            this.selectable?.Select();
            this.OnFocusSelected?.Invoke();
        }
        else
        {
            this.OnFocusDeselected?.Invoke();
        }
    }

    public virtual void Activate()
    {
        this.OnClick?.Invoke();
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        if (this.buttonManager != null)
        {
            this.buttonManager.FocusedButton = this;
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        this.OnSelect(eventData);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        this.buttonManager.OnButtonFocusChanged -= this.UpdateFocus;
    }
}


