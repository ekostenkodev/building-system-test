using System;
using Kadoy.BuildingSystem.Data.Scriptable;
using UnityEngine;

namespace Kadoy.BuildingSystem.Grid {
  public class BuildingGridField {
    public GridXZ<BuildingGridCell> Grid { get; }

    public BuildingGridField(GridAssets assets) {
      var creationCallback = new Func<int, int, BuildingGridCell>((x, y) => new BuildingGridCell(x, y));

      Grid = new GridXZ<BuildingGridCell>(assets.Width, assets.Height, assets.CellSize, Vector3.zero, creationCallback);
    }
  }
}