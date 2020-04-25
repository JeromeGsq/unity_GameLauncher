using InControl;
using System;
using System.Collections;
using System.Collections.Generic;
using Toastapp.DesignPatterns;
using ToastApp.GamePadInput;
using UnityEngine;

public class InControlHandler : GlobalSingleton<InControlHandler>
{
    public List<InputDevice> Controllers { get; set; } = new List<InputDevice>();

    public Action OnControllersChanged { get; set; }

    protected override void Awake()
    {
        base.Awake();
        this.StartCoroutine(this.Initialize());
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        foreach (var controller in this.Controllers)
        {
            Debug.Log(controller.Name);
        }
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("InControlHandler : Initialize OK!");
        InputManager.OnActiveDeviceChanged += inputDevice =>
        {
            if (!this.Controllers.Contains(inputDevice))
            {
                Debug.Log("InControlHandler : Add " + inputDevice.Name);

                this.Controllers.Add(inputDevice);

                ip_Debugger.Get.Controllers = this.Controllers;
                ip_Debugger.Get.ConfigureInput();

                this.OnControllersChanged?.Invoke();
            }
        };
    }

    public void SetPrimaryController(InputDevice primaryController)
    {
        this.Controllers?.Clear();
        if (!this.Controllers.Contains(primaryController))
        {
            Debug.Log("InControlHandler : Set primary controller " + primaryController.Name);

            this.Controllers.Add(primaryController);

            ip_Debugger.Get.Controllers = this.Controllers;
            ip_Debugger.Get.ConfigureInput();

            this.OnControllersChanged?.Invoke();
        }
    }

    public InputDevice GetController(int controllerIndex)
    {
        if (this.Controllers.Count > controllerIndex && controllerIndex >= 0)
        {
            return this.Controllers[controllerIndex];
        }
        else
        {
            return default(InputDevice);
        }
    }
}
