using Newtonsoft.Json;

public class GameItemData
{
    [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
    public string Title { get; set; }

    [JsonProperty("fileName", NullValueHandling = NullValueHandling.Ignore)]
    public string FileName { get; set; }

    [JsonProperty("executableLink", NullValueHandling = NullValueHandling.Ignore)]
    public string ExecutableLink { get; set; }
}