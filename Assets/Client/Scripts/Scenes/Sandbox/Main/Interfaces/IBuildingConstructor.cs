using System;
using Kadoy.BuildingSystem.Render;
using UnityEngine;

namespace Kadoy.BuildingSystem.Sandbox {
  public interface IBuildingConstructor {
    event Action<BuildingArgs> Constructed;
  }

  public struct BuildingArgs {
    public BuildingRender Building { get; }
    public Vector3 Position { get; }

    public BuildingArgs(BuildingRender building, Vector3 position) {
      Building = building;
      Position = position;
    }
  }
}