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
    private UISelectable focusedSelectable;

    public UISelectable FocusedButton { get => this.focusedSelectable; set => this.focusedSelectable = value; }

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

        if (this.FocusedButton == null)
        {
            this.focusedSelectable = this.GetComponentInChildren<UISelectable>();
            this.OnButtonFocusChanged?.Invoke(this.FocusedButton);
        }

        this.DelaySeconds(() => this.canMove = true, 0.2f, true);
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

    protected void MoveFocusTo(Selectable newFocusedSelectable)
    {
        this.canMove = false;
        if (newFocusedSelectable != null)
        {
            this.OnButtonFocusChanged.Invoke(newFocusedSelectable as UISelectable);
            this.focusedSelectable = newFocusedSelectable as UISelectable;
        }

        this.DelaySeconds(() => this.canMove = true, 0.05f, true);
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