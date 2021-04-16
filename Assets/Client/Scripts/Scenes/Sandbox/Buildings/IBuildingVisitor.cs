using Kadoy.BuildingSystem.Data.Model;

namespace Kadoy.BuildingSystem.Buildings {
  public interface IBuildingVisitor {
    void Visit(MiningBuildingModel mining);
    void Visit(StorageBuildingModel storage);
  }
}