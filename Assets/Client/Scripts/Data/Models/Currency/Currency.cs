using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kadoy.BuildingSystem.Data.Model {
  public class Currency {
    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public CurrencyType Type { get; private set; }

    [JsonProperty("count")]
    public int Count { get; private set; }

    public Currency(CurrencyType type, int count) {
      Type = type;
      Count = count;
    }
  }
}