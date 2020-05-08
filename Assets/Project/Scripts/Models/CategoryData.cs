using Newtonsoft.Json;
using System.Collections.Generic;

public class CategoryData
{
    [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
    public string Category { get; set; }

    [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
    public List<GameItemData> Items { get; set; }
}
