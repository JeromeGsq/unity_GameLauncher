using System;
using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityWeld.Binding;
using InControl;

public abstract class MainViewModel : BaseViewModel
{
    private float latitude = 50.6333f;
    private float longitude = 3.0667f;

    private string clock;
    private string devicesInfoCount;
    private Weather weather;

    [Binding]
    public string Clock
    {
        get => this.clock;
        set => this.Set(ref this.clock, value, nameof(this.Clock));
    }

    [Binding]
    public Weather Weather
    {
        get => this.weather;
        set => this.Set(ref this.weather, value, nameof(this.Weather));
    }

    [Binding]
    public string DevicesInfoCount
    {
        get => this.devicesInfoCount;
        set => this.Set(ref this.devicesInfoCount, value, nameof(this.DevicesInfoCount));
    }

    [Binding]
    public string CurrentDateTime => DateTime.Now.ToString("dddd, d MMMM yyyy");

    [Binding]
    public string WeatherTemp => $"{this.Weather?.Main?.Temp?.ToString("0.0") ?? "--"}°C";

    private void Start()
    {
        this.InvokeRepeating("UpdateTimers", 0, 1);
        this.StartCoroutine(this.GetWeather(this.latitude, this.longitude));
    }

    private void Update()
    {
        this.UpdateDevicesCount();
    }

    private void UpdateDevicesCount()
    {
        this.DevicesInfoCount = string.Empty;
        int iconIndex = 39;
        foreach (var devices in InputManager.Devices)
        {
            this.DevicesInfoCount += $"<sprite={iconIndex++}>  ";
        }

        if (string.IsNullOrEmpty(this.DevicesInfoCount))
        {
            this.DevicesInfoCount = "<size=75%>No controller</size>";
        }
    }

    private void UpdateTimers()
    {
        this.Clock = DateTime.Now.ToString("HH:mm:ss");
    }

    private IEnumerator GetWeather(float latitude, float longitude)
    {
        var url = $"https://fcc-weather-api.glitch.me/api/current?lat=" + latitude + "&lon=" + longitude;
        url = url.Replace(",", ".");
        var www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (!www.isNetworkError && !www.isHttpError)
        {
            try
            {
                string result = www.downloadHandler.text;
                Weather weather = JsonConvert.DeserializeObject<Weather>(result);
                this.Weather = weather;
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }

            this.RaisePropertyChanged(nameof(this.WeatherTemp));
        }
    }
}
