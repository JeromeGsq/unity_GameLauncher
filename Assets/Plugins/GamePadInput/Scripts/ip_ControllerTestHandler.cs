using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using InControl;

namespace ToastApp.GamepadInput
{
    public class ip_ControllerTestHandler : MonoBehaviour
    {
        private static float StickMultiply = 20.0f;

        #region Inspector Values
        [Space(20)]

        [SerializeField]
        private Image ControllerPlaceholder;

        [Space(20)]

        [SerializeField]
        private Text UnderLabel;

        [Space(20)]

        [SerializeField]
        private Image AButton;
        [SerializeField]
        private Image BButton;
        [SerializeField]
        private Image XButton;
        [SerializeField]
        private Image YButton;
        [SerializeField]
        private Image RBButton;
        [SerializeField]
        private Image LBButton;
        [SerializeField]
        private Image RTrigger;
        [SerializeField]
        private Image LTrigger;
        [SerializeField]
        private Image StartButton;
        [SerializeField]
        private Image SelectButton;
        [SerializeField]
        private GameObject UpButton;
        [SerializeField]
        private GameObject RightButton;
        [SerializeField]
        private GameObject DownButton;
        [SerializeField]
        private GameObject LeftButton;

        [Space(20)]

        [SerializeField]
        private Transform LeftStick;
        private Vector3 leftStickBasePosition;
        [SerializeField]
        private Transform RightStick;
        private Vector3 rightStickBasePosition;
        private InputDevice gamepadState;

        #endregion

        public Color MainColor { get; set; }

        public InControl.InputDevice GamepadState
        {
            get
            {
                return this.gamepadState;
            }
            set
            {
                this.gamepadState = value;
                this.UnderLabel.text = "Controller : " + this.gamepadState?.Name;
                this.gameObject.SetActive(true);
                this.UpdateColor();
            }
        }


        private void Awake()
        {
            this.gameObject.SetActive(false);
            this.rightStickBasePosition = this.RightStick.transform.localPosition;
            this.leftStickBasePosition = this.LeftStick.transform.localPosition;
        }

        private void UpdateColor()
        {
            this.ControllerPlaceholder.color = this.MainColor;
        }

        private void Update()
        {
            this.UnderLabel.text = "Controller : " + this.gamepadState?.Name;
            this.UpdateInputs();
        }

        private void UpdateInputs()
        {
            this.AButton.color = this.gamepadState.Cross.IsPressed ? Color.gray : Color.white;
            this.BButton.color = this.gamepadState.Circle.IsPressed ? Color.gray : Color.white;
            this.XButton.color = this.gamepadState.Square.IsPressed ? Color.gray : Color.white;
            this.YButton.color = this.gamepadState.Triangle.IsPressed ? Color.gray : Color.white;

            this.RBButton.color = this.gamepadState.R1 ? Color.gray : Color.white;
            this.LBButton.color = this.gamepadState.L1 ? Color.gray : Color.white;

            this.StartButton.color = this.gamepadState.StartOrSelect.IsPressed ? Color.gray : Color.white;
            this.SelectButton.color = this.gamepadState.StartOrSelect.IsPressed ? Color.gray : Color.white;

            this.RTrigger.color = this.gamepadState.R2.Value == 1 ? Color.gray : Color.white;
            this.LTrigger.color = this.gamepadState.L2.Value == 1 ? Color.gray : Color.white;

            this.UpButton.SetActive(this.gamepadState.DPadY.Value == 1);
            this.RightButton.SetActive(this.gamepadState.DPadX.Value == 1);
            this.DownButton.SetActive(this.gamepadState.DPadY.Value == -1);
            this.LeftButton.SetActive(this.gamepadState.DPadX.Value == -1);

            this.LeftStick.transform.localPosition = new Vector2(this.leftStickBasePosition.x + this.gamepadState.LeftStick.X * StickMultiply, this.leftStickBasePosition.y + this.gamepadState.LeftStick.Y * StickMultiply);
            this.RightStick.transform.localPosition = new Vector2(this.rightStickBasePosition.x + this.gamepadState.RightStick.X * StickMultiply, this.rightStickBasePosition.y + this.gamepadState.RightStick.Y * StickMultiply);

            this.RightStick.GetComponent<Image>().color = this.gamepadState.R2.IsPressed ? Color.gray : Color.white;
            this.LeftStick.GetComponent<Image>().color = this.gamepadState.L2.IsPressed ? Color.gray : Color.white;

            this.gamepadState.Vibrate(this.gamepadState.L2.Value, this.gamepadState.R2.Value);
        }
    }
}