using Kadoy.BuildingSystem.Data.Model;

namespace Kadoy.BuildingSystem.Services {
  public interface IBuildingService {
    UserModel Mining(MiningModel model);
    UserModel Storage(StorageModel model);
  }
}