using Kadoy.BuildingSystem.Data.Model;

namespace Kadoy.BuildingSystem.Services {
  public interface IShopService {
    bool IsAvailableToBuy(BuildingModel model);
    bool TryPurchaseBuilding(BuildingModel model, out UserModel user);
  }
}