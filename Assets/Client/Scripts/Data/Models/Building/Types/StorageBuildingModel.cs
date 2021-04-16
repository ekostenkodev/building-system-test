using Kadoy.BuildingSystem.Buildings;
using Newtonsoft.Json;

namespace Kadoy.BuildingSystem.Data.Model {
  public class StorageBuildingModel : BuildingModel {
    [JsonProperty("storage")]
    public StorageModel Storage { get; private set; }
    
    public override void Accept(IBuildingVisitor visitor) {
      visitor.Visit(this);
    }
  }
  
  public class StorageModel {
    [JsonProperty("currency")]
    public Currency Currency { get; private set; }
  }
}