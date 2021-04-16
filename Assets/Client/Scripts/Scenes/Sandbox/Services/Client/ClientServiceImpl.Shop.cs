using Kadoy.BuildingSystem.Data.Model;
using Kadoy.Util;

namespace Kadoy.BuildingSystem.Services  {
  internal partial class ClientServiceImpl : IShopService {
    public bool IsAvailableToBuy(BuildingModel model) {
      return model.Сonditions.Cost.All(cost => {
        var userCurrency = User.Currency.Current.FirstOrDefault(x => x.Type == cost.Type);

        return userCurrency != null && userCurrency.Count >= cost.Count;
      });
    }
    
    public bool TryPurchaseBuilding(BuildingModel model, out UserModel newUser) {
      var userCurrency = User.Currency;
      var newCurrency = new Currency[User.Currency.Current.Length];

      for (var i = 0; i < newCurrency.Length; i++) {
        var current = userCurrency.Current[i];
        var cost = model.Сonditions.Cost.FirstOrDefault(c=>c.Type == current.Type);
        var value = current.Count;

        if (cost != null) {
          value = current.Count - cost.Count;

          if (value < 0) {
            newUser = User;
            return false;
          }
        }

        newCurrency[i] = new Currency(current.Type, value);
      }

      newUser = new UserModel(new UserCurrency(newCurrency, User.Currency.Max));

      return true;
    }
  }
}