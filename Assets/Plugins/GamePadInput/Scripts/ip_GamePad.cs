using UnityEngine;

namespace ToastApp.GamepadInput
{
	public static class ip_GamePad
	{
		public static Index KeyboardIndex = Index.Any;

		public enum Button
		{
			A, B, Y, X, RightShoulder, LeftShoulder, RightStick, LeftStick, Back, Start
		}
		public enum Trigger
		{
			LeftTrigger, RightTrigger
		}
		public enum Axis
		{
			LeftStick, RightStick, Dpad
		}
		public enum Index
		{
			One = 0, Two = 1, Three = 2, Four = 3, Any = 4,
        }

		public static bool GetButton(Button button, Index controlIndex)
		{
			KeyCode code = GetKeycode(button, controlIndex);
			return Input.GetKey(code);
		}

		public static bool GetButton(KeyCode key, Index controlIndex)
		{
			if(KeyboardIndex == Index.Any)
			{
				return Input.GetKey(key);
			}
			else if(KeyboardIndex == controlIndex)
			{
				return Input.GetKey(key);
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// returns a specified axis
		/// </summary>
		/// <param name="axis">One of the analogue sticks, or the dpad</param>
		/// <param name="controlIndex">The controller number</param>
		/// <param name="raw">if raw is false then the controlIndex will be returned with a deadspot</param>
		/// <returns></returns>
		public static Vector2 GetAxis(Axis axis, Index controlIndex, bool raw = false)
		{
			string xName = "";
			string yName = "";
			switch(axis)
			{
				case Axis.Dpad:
					xName = "DPad_XAxis_" + (int)controlIndex;
					yName = "DPad_YAxis_" + (int)controlIndex;
					break;
				case Axis.LeftStick:
					xName = "L_XAxis_" + (int)controlIndex;
					yName = "L_YAxis_" + (int)controlIndex;
					break;
				case Axis.RightStick:
					xName = "R_XAxis_" + (int)controlIndex;
					yName = "R_YAxis_" + (int)controlIndex;
					break;
			}

			Vector2 axisXY = Vector3.zero;

			try
			{
				if(raw == false)
				{
					axisXY.x = Input.GetAxis(xName);
					axisXY.y = -Input.GetAxis(yName);
				}
				else
				{
					axisXY.x = Input.GetAxisRaw(xName);
					axisXY.y = -Input.GetAxisRaw(yName);
				}
			}
			catch(System.Exception e)
			{
				Debug.LogError(e);
				Debug.LogWarning("Have you set up all axes correctly? \nThe easiest solution is to replace the InputManager.asset with version located in the GamepadInput package. \nWarning: do so will overwrite any existing input");
			}
			return axisXY;
		}

		public static float GetTrigger(Trigger trigger, Index controlIndex, bool raw = false)
		{
			string name = "";
			if(trigger == Trigger.LeftTrigger)
				name = "TriggersL_" + (int)controlIndex;
			else if(trigger == Trigger.RightTrigger)
				name = "TriggersR_" + (int)controlIndex;

			float axis = 0;
			try
			{
				if(raw == false)
					axis = Input.GetAxis(name);
				else
					axis = Input.GetAxisRaw(name);
			}
			catch(System.Exception e)
			{
				Debug.LogError(e);
				Debug.LogWarning("Have you set up all axes correctly? \nThe easiest solution is to replace the InputManager.asset with version located in the GamepadInput package. \nWarning: do so will overwrite any existing input");
			}
			return axis;
		}

		static KeyCode GetKeycode(Button button, Index controlIndex)
		{
			switch(controlIndex)
			{
				case Index.One:
					switch(button)
					{
						case Button.A:
							return KeyCode.Joystick1Button0;
						case Button.B:
							return KeyCode.Joystick1Button1;
						case Button.X:
							return KeyCode.Joystick1Button2;
						case Button.Y:
							return KeyCode.Joystick1Button3;
						case Button.RightShoulder:
							return KeyCode.Joystick1Button5;
						case Button.LeftShoulder:
							return KeyCode.Joystick1Button4;
						case Button.Back:
							return KeyCode.Joystick1Button6;
						case Button.Start:
							return KeyCode.Joystick1Button7;
						case Button.LeftStick:
							return KeyCode.Joystick1Button8;
						case Button.RightStick:
							return KeyCode.Joystick1Button9;
					}
					break;

				case Index.Two:
					switch(button)
					{
						case Button.A:
							return KeyCode.Joystick2Button0;
						case Button.B:
							return KeyCode.Joystick2Button1;
						case Button.X:
							return KeyCode.Joystick2Button2;
						case Button.Y:
							return KeyCode.Joystick2Button3;
						case Button.RightShoulder:
							return KeyCode.Joystick2Button5;
						case Button.LeftShoulder:
							return KeyCode.Joystick2Button4;
						case Button.Back:
							return KeyCode.Joystick2Button6;
						case Button.Start:
							return KeyCode.Joystick2Button7;
						case Button.LeftStick:
							return KeyCode.Joystick2Button8;
						case Button.RightStick:
							return KeyCode.Joystick2Button9;
					}
					break;

				case Index.Three:
					switch(button)
					{
						case Button.A:
							return KeyCode.Joystick3Button0;
						case Button.B:
							return KeyCode.Joystick3Button1;
						case Button.X:
							return KeyCode.Joystick3Button2;
						case Button.Y:
							return KeyCode.Joystick3Button3;
						case Button.RightShoulder:
							return KeyCode.Joystick3Button5;
						case Button.LeftShoulder:
							return KeyCode.Joystick3Button4;
						case Button.Back:
							return KeyCode.Joystick3Button6;
						case Button.Start:
							return KeyCode.Joystick3Button7;
						case Button.LeftStick:
							return KeyCode.Joystick3Button8;
						case Button.RightStick:
							return KeyCode.Joystick3Button9;
					}
					break;

				case Index.Four:
					switch(button)
					{
						case Button.A:
							return KeyCode.Joystick4Button0;
						case Button.B:
							return KeyCode.Joystick4Button1;
						case Button.X:
							return KeyCode.Joystick4Button2;
						case Button.Y:
							return KeyCode.Joystick4Button3;
						case Button.RightShoulder:
							return KeyCode.Joystick4Button5;
						case Button.LeftShoulder:
							return KeyCode.Joystick4Button4;
						case Button.Back:
							return KeyCode.Joystick4Button6;
						case Button.Start:
							return KeyCode.Joystick4Button7;
						case Button.LeftStick:
							return KeyCode.Joystick4Button8;
						case Button.RightStick:
							return KeyCode.Joystick4Button9;
					}
					break;

				case Index.Any:
					switch(button)
					{
						case Button.A:
							return KeyCode.JoystickButton0;
						case Button.B:
							return KeyCode.JoystickButton1;
						case Button.X:
							return KeyCode.JoystickButton2;
						case Button.Y:
							return KeyCode.JoystickButton3;
						case Button.RightShoulder:
							return KeyCode.JoystickButton5;
						case Button.LeftShoulder:
							return KeyCode.JoystickButton4;
						case Button.Back:
							return KeyCode.JoystickButton6;
						case Button.Start:
							return KeyCode.JoystickButton7;
						case Button.LeftStick:
							return KeyCode.JoystickButton8;
						case Button.RightStick:
							return KeyCode.JoystickButton9;
					}
					break;
			}
			return KeyCode.None;
		}

		public static void GetState(ref GamepadState oldState, Index controlIndex, bool raw = false)
		{
			if(oldState == null)
			{
				return;
			}

			GamepadState state = new GamepadState();

			// Controller
			state.A = GetButton(Button.A, controlIndex);
			state.B = GetButton(Button.B, controlIndex);
			state.Y = GetButton(Button.Y, controlIndex);
			state.X = GetButton(Button.X, controlIndex);

			state.RB = GetButton(Button.RightShoulder, controlIndex);
			state.LB = GetButton(Button.LeftShoulder, controlIndex);
			state.RightStickClick = GetButton(Button.RightStick, controlIndex);
			state.LeftStickClick = GetButton(Button.LeftStick, controlIndex);

			state.Start = GetButton(Button.Start, controlIndex);
			state.Back = GetButton(Button.Back, controlIndex);

			state.LeftStickAxis = GetAxis(Axis.LeftStick, controlIndex, raw);
			state.RightStickAxis = GetAxis(Axis.RightStick, controlIndex, raw);
			state.dPadAxis = GetAxis(Axis.Dpad, controlIndex, raw);

			state.Left = (state.dPadAxis.x < -0.1f);
			state.Right = (state.dPadAxis.x > 0.1f);
			state.Up = (state.dPadAxis.y > 0.1f);
			state.Down = (state.dPadAxis.y < -0.1f);

			state.LT = GetTrigger(Trigger.LeftTrigger, controlIndex, raw);
			state.RT = GetTrigger(Trigger.RightTrigger, controlIndex, raw);

			// Keyboard
			state.A = state.A == false ? GetButton(KeyCode.L, controlIndex) : true;
			state.B = state.B == false ? GetButton(KeyCode.M, controlIndex) : true;
			state.X = state.X == false ? GetButton(KeyCode.O, controlIndex) : true;
			state.Y = state.Y == false ? GetButton(KeyCode.P, controlIndex) : true;

			state.LB = state.LB == false ? GetButton(KeyCode.A, controlIndex) : true;
			state.RB = state.RB == false ? GetButton(KeyCode.E, controlIndex) : true;

			state.Start = state.Start == false ? GetButton(KeyCode.Return, controlIndex) : true;
			state.Back = state.Back == false ? GetButton(KeyCode.Backspace, controlIndex) : true;

			if(state.LeftStickAxis.x == 0)
			{
				if(GetButton(KeyCode.D, controlIndex))
				{
					state.LeftStickAxis.x = 1;
				}
				else if(GetButton(KeyCode.Q, controlIndex))
				{
					state.LeftStickAxis.x = -1;
				}
			}

			if(state.LeftStickAxis.y == 0)
			{
				if(GetButton(KeyCode.Z, controlIndex))
				{
					state.LeftStickAxis.y = 1;
				}
				else if(GetButton(KeyCode.S, controlIndex))
				{
					state.LeftStickAxis.y = -1;
				}
			}

			if(state.RightStickAxis.x == 0)
			{
				if(GetButton(KeyCode.H, controlIndex))
				{
					state.RightStickAxis.x = 1;
				}
				else if(GetButton(KeyCode.F, controlIndex))
				{
					state.RightStickAxis.x = -1;
				}
			}

			if(state.RightStickAxis.y == 0)
			{
				if(GetButton(KeyCode.T, controlIndex))
				{
					state.RightStickAxis.y = 1;
				}
				else if(GetButton(KeyCode.G, controlIndex))
				{
					state.RightStickAxis.y = -1;
				}
			}

			state.Down = state.Down == false ? GetButton(KeyCode.DownArrow, controlIndex) : true;
			state.Left = state.Left == false ? GetButton(KeyCode.LeftArrow, controlIndex) : true;
			state.Right = state.Right == false ? GetButton(KeyCode.RightArrow, controlIndex) : true;
			state.Up = state.Up == false ? GetButton(KeyCode.UpArrow, controlIndex) : true;

			state.LT = state.LT == 0 ? GetButton(KeyCode.W, controlIndex) ? 1 : 0 : 1;
			state.RT = state.RT == 0 ? GetButton(KeyCode.X, controlIndex) ? 1 : 0 : 1;

			// Old pad pressed
			state.APressed = state.A;
			state.BPressed = state.B;
			state.XPressed = state.X;
			state.YPressed = state.Y;

			state.RBPressed = state.RB;
			state.LBPressed = state.LB;

			state.RTPressed = state.RT == 1;
			state.LTPressed = state.RT == 1;

			state.UpPressed = state.Up;
			state.RightPressed = state.Right;
			state.LeftPressed = state.Left;
			state.DownPressed = state.Down;

			state.LStickUpPressed = state.LeftStickAxis.y > 0.8f;
			state.LStickDownPressed = state.LeftStickAxis.y < -0.8f;
			state.LStickRightPressed = state.LeftStickAxis.x > 0.8f;
			state.LStickLeftPressed = state.LeftStickAxis.x < -0.8f;

			if(oldState.Up == true)
			{
				state.UpPressed = false;
			}
			if(oldState.Left == true)
			{
				state.LeftPressed = false;
			}
			if(oldState.Right == true)
			{
				state.RightPressed = false;
			}
			if(oldState.Down == true)
			{
				state.DownPressed = false;
			}

			if(oldState.A == true)
			{
				state.APressed = false;
			}
			if(oldState.B == true)
			{
				state.BPressed = false;
			}
			if(oldState.X == true)
			{
				state.XPressed = false;
			}
			if(oldState.Y == true)
			{
				state.YPressed = false;
			}

			if(oldState.RB == true)
			{
				state.RBPressed = false;
			}
			if(oldState.LB == true)
			{
				state.LBPressed = false;
			}
			if(oldState.RT == 1)
			{
				state.RTPressed = false;
			}
			if(oldState.RB == true)
			{
				state.RBPressed = false;
			}

			if(oldState.LeftStickAxis.y > 0.8f)
			{
				state.LStickUpPressed = false;
			}
			if(oldState.LeftStickAxis.y < -0.8f)
			{
				state.LStickDownPressed = false;
			}
			if(oldState.LeftStickAxis.x > 0.8f)
			{
				state.LStickRightPressed = false;
			}
			if(oldState.LeftStickAxis.x < -0.8f)
			{
				state.LStickLeftPressed = false;
			}

			oldState = state;
		}
	}

	public class GamepadState
	{
		public bool APressed = false;
		public bool BPressed = false;
		public bool XPressed = false;
		public bool YPressed = false;
		public bool StartPressed = false;
		public bool BackPressed = false;
		public bool RBPressed = false;
		public bool LBPressed = false;
		public bool RTPressed = false;
		public bool LTPressed = false;

		public bool UpPressed;
		public bool DownPressed;
		public bool RightPressed;
		public bool LeftPressed;

		public bool LStickUpPressed;
		public bool LStickDownPressed;
		public bool LStickLeftPressed;
		public bool LStickRightPressed;

		public bool RStickUpPressed;
		public bool RStickDownPressed;
		public bool RStickLeftPressed;
		public bool RStickRightPressed;

		public bool A = false;
		public bool B = false;
		public bool X = false;
		public bool Y = false;
		public bool Start = false;
		public bool Back = false;
		public bool Left = false;
		public bool Right = false;
		public bool Up = false;
		public bool Down = false;

		public bool LeftStickClick = false;
		public bool RightStickClick = false;
		public bool LB = false;
		public bool RB = false;

		public Vector2 LeftStickAxis = Vector2.zero;
		public Vector2 RightStickAxis = Vector2.zero;
		public Vector2 dPadAxis = Vector2.zero;

		public float LT = 0;
		public float RT = 0;
	}
}