using InControl;
using System.Collections;
using System.Collections.Generic;
using Toastapp.DesignPatterns;
using ToastApp.GamepadInput;
using UnityEngine;

namespace ToastApp.GamePadInput
{
    public class ip_Debugger : GlobalSingleton<ip_Debugger>
    {
        private static List<Color> ColorList = new List<Color> {
		    // BLUE
		    new Color(0.329f, 0.388f, 1f),
		    // RED
		    new Color(0.961f, 0.18f, 0.18f),
		    // GREEN
		    new Color(0.122f, 0.62f, 0.251f),
		    // YELLOW
		    new Color(1, 0.78f, 0.09f),
            // CLEAR
		    new Color(0, 0, 0),
        };

        [SerializeField]
        private GameObject panel;

        [Space(20)]

        [SerializeField]
        private ip_ControllerTestHandler controllerTestHandlerOne;
        [SerializeField]
        private ip_ControllerTestHandler controllerTestHandlerTwo;
        [SerializeField]
        private ip_ControllerTestHandler controllerTestHandlerThree;
        [SerializeField]
        private ip_ControllerTestHandler controllerTestHandlerFour;

        public List<InputDevice> Controllers { get; set; } = new List<InputDevice>();

        public void ConfigureInput()
        {
            if (this.controllerTestHandlerOne != null && this.Controllers.Count >= 1 && this.Controllers[0] != null)
            {
                this.controllerTestHandlerOne.MainColor = ColorList[0];
                this.controllerTestHandlerOne.GamepadState = this.Controllers[0];
            }

            if (this.controllerTestHandlerTwo != null && this.Controllers.Count >= 2 && this.Controllers[1] != null)
            {
                this.controllerTestHandlerTwo.MainColor = ColorList[1];
                this.controllerTestHandlerTwo.GamepadState = this.Controllers[1];
            }

            if (this.controllerTestHandlerThree != null && this.Controllers.Count >= 3 && this.Controllers[2] != null)
            {
                this.controllerTestHandlerThree.MainColor = ColorList[2];
                this.controllerTestHandlerThree.GamepadState = this.Controllers[2];
            }

            if (this.controllerTestHandlerFour != null && this.Controllers.Count >= 4 && this.Controllers[3] != null)
            {
                this.controllerTestHandlerFour.MainColor = ColorList[3];
                this.controllerTestHandlerFour.GamepadState = this.Controllers[3];
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) ||
                InputManager.ActiveDevice.L1 && InputManager.ActiveDevice.R1 && InputManager.ActiveDevice.CommandWasPressed)
            {
                this.panel.SetActive(!this.panel.activeSelf);
                this.ConfigureInput();
            }
        }
    }
}