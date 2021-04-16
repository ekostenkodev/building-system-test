using System;
using Kadoy.BuildingSystem.Data.Model;

namespace Kadoy.BuildingSystem.Sandbox {
  public interface IBuildingSelector {
    event Action<BuildingModel> Selected;
  }
}