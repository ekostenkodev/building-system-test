using Newtonsoft.Json;

namespace Kadoy.BuildingSystem.Data.Model {
  public class UserCurrency {
    [JsonProperty("current")]
    public Currency[] Current { get; private set; }
    
    [JsonProperty("max")]
    public Currency[] Max { get; private set; }
      
    public UserCurrency(Currency[] current, Currency[] max) {
      Current = current;
      Max = max;
    }
  }
}