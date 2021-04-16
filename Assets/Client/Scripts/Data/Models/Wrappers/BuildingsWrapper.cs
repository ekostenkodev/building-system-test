using Newtonsoft.Json;

namespace Kadoy.BuildingSystem.Data.Model {
  public class BuildingsWrapper {
    [JsonProperty("buildings")]
    public BuildingListModel Buildings { get; private set; }
  }
}