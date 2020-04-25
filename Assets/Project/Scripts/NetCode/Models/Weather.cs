using System.Collections.Generic;
using Newtonsoft.Json;

public partial class Weather
{
    [JsonProperty("coord", NullValueHandling = NullValueHandling.Ignore)]
    public Coord Coord { get; set; }

    [JsonProperty("weather", NullValueHandling = NullValueHandling.Ignore)]
    public List<WeatherElement> WeatherWeather { get; set; }

    [JsonProperty("base", NullValueHandling = NullValueHandling.Ignore)]
    public string Base { get; set; }

    [JsonProperty("main", NullValueHandling = NullValueHandling.Ignore)]
    public Main Main { get; set; }

    [JsonProperty("visibility", NullValueHandling = NullValueHandling.Ignore)]
    public long? Visibility { get; set; }

    [JsonProperty("wind", NullValueHandling = NullValueHandling.Ignore)]
    public Wind Wind { get; set; }

    [JsonProperty("clouds", NullValueHandling = NullValueHandling.Ignore)]
    public Clouds Clouds { get; set; }

    [JsonProperty("dt", NullValueHandling = NullValueHandling.Ignore)]
    public long? Dt { get; set; }

    [JsonProperty("sys", NullValueHandling = NullValueHandling.Ignore)]
    public Sys Sys { get; set; }

    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public long? Id { get; set; }

    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("cod", NullValueHandling = NullValueHandling.Ignore)]
    public long? Cod { get; set; }
}

public partial class Clouds
{
    [JsonProperty("all", NullValueHandling = NullValueHandling.Ignore)]
    public long? All { get; set; }
}

public partial class Coord
{
    [JsonProperty("lon", NullValueHandling = NullValueHandling.Ignore)]
    public long? Lon { get; set; }

    [JsonProperty("lat", NullValueHandling = NullValueHandling.Ignore)]
    public long? Lat { get; set; }
}

public partial class Main
{
    [JsonProperty("temp", NullValueHandling = NullValueHandling.Ignore)]
    public double? Temp { get; set; }

    [JsonProperty("pressure", NullValueHandling = NullValueHandling.Ignore)]
    public long? Pressure { get; set; }

    [JsonProperty("humidity", NullValueHandling = NullValueHandling.Ignore)]
    public long? Humidity { get; set; }

    [JsonProperty("temp_min", NullValueHandling = NullValueHandling.Ignore)]
    public long? TempMin { get; set; }

    [JsonProperty("temp_max", NullValueHandling = NullValueHandling.Ignore)]
    public long? TempMax { get; set; }
}

public partial class Sys
{
    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    public long? Type { get; set; }

    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public long? Id { get; set; }

    [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
    public double? Message { get; set; }

    [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
    public string Country { get; set; }

    [JsonProperty("sunrise", NullValueHandling = NullValueHandling.Ignore)]
    public long? Sunrise { get; set; }

    [JsonProperty("sunset", NullValueHandling = NullValueHandling.Ignore)]
    public long? Sunset { get; set; }
}

public partial class WeatherElement
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public long? Id { get; set; }

    [JsonProperty("main", NullValueHandling = NullValueHandling.Ignore)]
    public string Main { get; set; }

    [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
    public string Description { get; set; }
}

public partial class Wind
{
    [JsonProperty("speed", NullValueHandling = NullValueHandling.Ignore)]
    public double? Speed { get; set; }

    [JsonProperty("deg", NullValueHandling = NullValueHandling.Ignore)]
    public long? Deg { get; set; }
}