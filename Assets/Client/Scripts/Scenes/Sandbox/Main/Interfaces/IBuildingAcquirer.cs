using System;

namespace Kadoy.BuildingSystem.Sandbox {
  public interface IBuildingAcquirer {
    event Action<PlaceArgs> Acquired;
  }
}