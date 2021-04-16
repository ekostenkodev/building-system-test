using Kadoy.BuildingSystem.Buildings;
using Newtonsoft.Json;

namespace Kadoy.BuildingSystem.Data.Model {
  public class MiningBuildingModel : BuildingModel {
    [JsonProperty("mining")]
    public MiningModel Mining { get; private set; }

    public override void Accept(IBuildingVisitor visitor) {
      visitor.Visit(this);
    }
  }
  
  public class MiningModel {
    [JsonProperty("currency")]
    public Currency Currency { get; private set; }
    
    [JsonProperty("delay")]
    public float Delay { get; private set; }
  }
}