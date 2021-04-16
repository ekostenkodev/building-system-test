using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kadoy.BuildingSystem.Data.Model {
  public class BuildingListModel {
    [JsonProperty("mining")]
    public IEnumerable<MiningBuildingModel> Mining { get; private set; }
    
    [JsonProperty("storage")]
    public IEnumerable<StorageBuildingModel> Storage { get; private set; }
  }
}