using System.Globalization;
using System.Threading;
using UnityEngine;
using Toastapp.MVVM;

public class App : MonoBehaviour
{
    protected void Start()
    {
        var ci = new CultureInfo("fr-FR");
        Thread.CurrentThread.CurrentCulture = ci;
        Thread.CurrentThread.CurrentUICulture = ci;

        Application.targetFrameRate = 60;

#if !UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#endif

        NavigationService.Get.ShowViewModel(typeof(SplashViewModel));
    }

    private void OnApplicationFocus(bool focus)
    {
#if !UNITY_EDITOR
     Application.targetFrameRate = focus ? 60 : 1;
#else
        Application.targetFrameRate = 60;
#endif
    }
}