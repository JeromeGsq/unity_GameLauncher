using System.Globalization;
using System.Threading;
using UnityEngine;
using Toastapp.MVVM;
using Toastapp.DesignPatterns;

public class App : SceneSingleton<App>
{
    private bool gameIsLaunched;

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

        if (focus && this.gameIsLaunched)
        {
            this.DelaySeconds(() =>
            {
                this.gameIsLaunched = false;
                NavigationService.Get.ShowViewModel(typeof(SplashViewModel));
            }, 2);
        }
    }

    public void LaunchGame()
    {
        this.DelaySeconds(() =>
        {
            this.gameIsLaunched = true;
            NavigationService.Get.ClearNavigationStack();
        }, 5);
    }
}