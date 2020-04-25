using InControl;

public class UISelectableTabsManager : UISelectableManager
{
    protected override void ListenEvents(InputDevice gamepadState)
    {
        // Move
        if (gamepadState.R1.WasPressed)
        {
            this.MoveFocusTo(this.FocusedButton.Right);

        }
        else if (gamepadState.L1.WasPressed)
        {
            this.MoveFocusTo(this.FocusedButton.Left);
        }

        // Press button
        this.FocusedButton.Activate();
    }
}
