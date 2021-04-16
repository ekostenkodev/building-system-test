using Kadoy.BuildingSystem.Data.Model;
using UnityEngine;

namespace Kadoy.BuildingSystem.Sandbox {
  public struct PlaceArgs {
    public BuildingModel Building { get; }
    public Vector3 Position { get; }

    public PlaceArgs(BuildingModel building, Vector3 position) {
      Building = building;
      Position = position;
    }
  }
}