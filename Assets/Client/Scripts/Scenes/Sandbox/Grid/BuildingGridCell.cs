using Kadoy.BuildingSystem.Render;

namespace Kadoy.BuildingSystem.Grid {
  public class BuildingGridCell {
    public int X { get; }
    public int Y { get; }
    public BuildingRender Placed { get; set; }

    public bool IsEmpty => Placed == null;

    public BuildingGridCell(int x, int y) {
      X = x;
      Y = y;
    }
  }
}