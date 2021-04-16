using Newtonsoft.Json;

namespace Kadoy.BuildingSystem.Data.Model {
  public class UserModel {
    [JsonProperty("currency")]
    public UserCurrency Currency { get; private set; }

    public UserModel(UserCurrency currency) {
      Currency = currency;
    }
  }
}