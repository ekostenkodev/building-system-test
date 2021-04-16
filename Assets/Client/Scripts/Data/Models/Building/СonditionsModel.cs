using Newtonsoft.Json;

namespace Kadoy.BuildingSystem.Data.Model {
  public class СonditionsModel {
    [JsonProperty("buildDuration")]
    public float BuildDuration { get; private set; }
    
    [JsonProperty("cost")]
    public Currency[] Cost { get; private set; }
  }
}