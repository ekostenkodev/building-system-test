using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Services;
using UnityEngine;

namespace Kadoy.BuildingSystem.Services  {
  internal partial class ClientServiceImpl : IBuildingService {
    public UserModel Mining(MiningModel model) {
      var lastCurrent = User.Currency.Current;
      var lastMax = User.Currency.Max;
      var newCurrent = new Currency[lastCurrent.Length];

      for (var i = 0; i < lastCurrent.Length; i++) {
        var type = lastCurrent[i].Type;
        var isTargetType = type == model.Currency.Type;
        var current = lastCurrent[i].Count;
        var max = lastMax[i].Count;
        
        if (isTargetType) {
          current = Mathf.Clamp(current + model.Currency.Count, 0, max);
        }

        newCurrent[i] = new Currency(type, current);
      }

      return new UserModel(new UserCurrency(newCurrent, User.Currency.Max));
    }

    public UserModel Storage(StorageModel model) {
      var lastMax = User.Currency.Max;
      var newMax = new Currency[lastMax.Length];

      for (var i = 0; i < lastMax.Length; i++) {
        var type = lastMax[i].Type;
        var isTargetType = type == model.Currency.Type;
        var value = lastMax[i].Count;
        
        if (isTargetType) {
          value += model.Currency.Count;
        }

        newMax[i] = new Currency(type, value);
      }

      return new UserModel(new UserCurrency(User.Currency.Current, newMax));
    }
  }
}