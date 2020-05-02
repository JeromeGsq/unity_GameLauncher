using InControl;
using System;
using Toastapp.MVVM;
using UnityEngine;
using UnityEngine.UI;

public class UISelectableManager : MonoBehaviour
{
    private IViewModel viewModel;

    private bool canMove = false;

    [SerializeField]
    private UISelectable defaultFocusedSelectable;

    private UISelectable focusedButton;
    public UISelectable FocusedButton { get => this.focusedButton; set => this.focusedButton = value; }

    public Action<UISelectable> OnButtonFocusChanged { get; set; }

    private void Awake()
    {
        var components = this.GetComponentsInParent(typeof(Component));
        foreach (var component in components)
        {
            var interfaces = component.GetType().GetInterfaces();
            foreach (var inter in interfaces)
            {
                if (inter.Equals(typeof(IViewModel)))
                {
                    this.viewModel = component as IViewModel;
                    break;
                }
            }
        }

        if (this.defaultFocusedSelectable == null)
        {
            this.FocusedButton = this.GetComponentInChildren<UISelectable>();
        }
        else
        {
            this.FocusedButton = this.defaultFocusedSelectable;
        }
    }

    public void OnEnable()
    {
        this.DelaySeconds(() =>
        {
            this.canMove = true;

            // After this.FocusedButton.SetActive(true), wake-up the FocusedButton after 200ms
            this.MoveFocusTo(this.FocusedButton);
        }, 0.2f, true);
    }

    private void Update()
    {
        var gamepadState = InputManager.ActiveDevice;

        gamepadState.LeftStick.LowerDeadZone = 0.8f;
        gamepadState.RightStick.LowerDeadZone = 0.8f;

        if (this.viewModel?.IsInBackground == true)
        {
            return;
        }
        if (this.canMove == false)
        {
            return;
        }

        this.ListenEvents(gamepadState);
    }

    public void GainFocus()
    {
        this.canMove = true;
    }

    public void LoseFocus()
    {
        this.canMove = false;
    }

    public void ResumeFocusOnLastSelectable()
    {
        this.MoveFocusTo(this.FocusedButton);
    }

    protected void MoveFocusTo(Selectable newFocusedSelectable)
    {
        this.canMove = false;
        if (newFocusedSelectable != null)
        {
            this.OnButtonFocusChanged?.Invoke(newFocusedSelectable as UISelectable);
            this.FocusedButton = (newFocusedSelectable as UISelectable);
        }

        this.DelaySeconds(() => this.canMove = true, 0.05f, true);
    }

    public void AssignDefaultSelectable(UISelectable uiSelectable)
    {
        this.defaultFocusedSelectable = uiSelectable;
        this.FocusedButton = uiSelectable;
    }

    protected virtual void ListenEvents(InputDevice gamepadState)
    {
        // Press A 
        if (gamepadState.Cross.WasPressed)
        {
            this.FocusedButton.Activate();
        }

        // Move
        if (gamepadState.LeftStick.Up.WasPressed || gamepadState.DPadUp.WasPressed)
        {
            this.MoveFocusTo(this.FocusedButton.Up);
        }
        else if (gamepadState.LeftStick.Down.WasPressed || gamepadState.DPadDown.WasPressed)
        {
            this.MoveFocusTo(this.FocusedButton.Down);
        }
        else if (gamepadState.LeftStick.Left.WasPressed || gamepadState.DPadLeft.WasPressed)
        {
            this.MoveFocusTo(this.FocusedButton.Left);
        }
        else if (gamepadState.LeftStick.Right.WasPressed || gamepadState.DPadRight.WasPressed)
        {
            this.MoveFocusTo(this.FocusedButton.Right);
        }
    }
}