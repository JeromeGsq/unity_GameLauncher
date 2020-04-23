using System;
using System.Threading;
using UnityWeld.Binding;

[Binding]
public class SplashViewModel : BaseViewModel
{
    private string clock;

    [Binding]
    public string Clock
    {
        get => this.clock;
        set => this.Set(ref this.clock, value, nameof(this.Clock));
    }

    private void Start()
    {
        this.InvokeRepeating("UpdateClock", 0, 1);
    }

    private void UpdateClock()
    {
        this.Clock = DateTime.Now.ToString("HH:mm:ss");
    }
}
