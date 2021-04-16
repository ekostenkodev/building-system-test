using System;

namespace Kadoy.BuildingSystem.Sandbox {
  public interface IPlaceSelector {
    event Action<PlaceArgs> Placed;
  }
}