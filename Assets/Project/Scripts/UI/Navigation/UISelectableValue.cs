using InControl;
using UnityEngine;
using static UnityEngine.UI.Button;

public class UISelectableValue : UISelectable
{
    [SerializeField]
    private ButtonClickedEvent onLeft;

    [SerializeField]
    private ButtonClickedEvent onRight;

    public ButtonClickedEvent OnLeft { get => onLeft; set => onLeft = value; }
    public ButtonClickedEvent OnRight { get => onRight; set => onRight = value; }

    private void FixedUpdate()
    {
        if (this.HasFocus == false)
        {
            return;
        }

        if (InputManager.ActiveDevice.LeftStick.Left.WasPressed || InputManager.ActiveDevice.DPadLeft.WasPressed)
        {
            this.onLeft.Invoke();
        }
        else if (InputManager.ActiveDevice.LeftStick.Right.WasPressed || InputManager.ActiveDevice.DPadRight.WasPressed)
        {
            this.onRight.Invoke();
        }
    }
}