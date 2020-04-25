// ToastAppStudio - JeromeGsq 
// Version : 1.0
Replace ./InputManager.asset into ./ProjectSettings/InputManager.asset


// How to use : code 
public class Test : Monobehavior{

	private GamepadState gamepadState;

    private void Awake()
    {
        this.gamepadState = new GamepadState();
	}

    private void Update()
    {
        ip_GamePad.GetState(ref this.gamepadState, this.index);

		if(this.gamepadState.A){
			Debug.Log("A is pressed");
		}

		if(this.gamepadState.APressed){
			Debug.Log("A is pressed one time");
		}
	}
}