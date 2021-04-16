using Newtonsoft.Json;

namespace Kadoy.BuildingSystem.Data.Model {
  public class UserWrapper {
    [JsonProperty("user")]
    public UserModel User { get; private set; }
  }
}