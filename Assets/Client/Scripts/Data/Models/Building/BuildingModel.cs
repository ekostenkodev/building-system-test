using Kadoy.BuildingSystem.Buildings;
using Newtonsoft.Json;

namespace Kadoy.BuildingSystem.Data.Model {
  public abstract class BuildingModel {
    [JsonProperty("id")]
    public string Id { get; private set; }
    
    [JsonProperty("level")]
    public int Level { get; private set; }
    
    [JsonProperty("building")]
    public СonditionsModel Сonditions { get; private set; }

    public abstract void Accept(IBuildingVisitor visitor);
  }
}