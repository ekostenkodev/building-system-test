using Kadoy.BuildingSystem.Data.Scriptable;
using Kadoy.BuildingSystem.Grid;
using UnityEngine;

namespace Kadoy.BuildingSystem.Sandbox.Controllers {
  public class FielderConstructorController : MonoBehaviour, IFielder {
    private IBuildingConstructor constructor;
    private GridAssets gridAssets;
    
    public BuildingGridField Field { get; private set; }

    private void OnDisable() {
      if (constructor != null) {
        constructor.Constructed -= OnConstructed;
      }
    }

    public void Inject(IBuildingConstructor constructor, GridAssets gridAssets) {
      this.constructor = constructor;
      this.gridAssets = gridAssets;
    }

    public void Initialize() {
      Field = new BuildingGridField(gridAssets);

      constructor.Constructed += OnConstructed;
    }

    private void OnConstructed(BuildingArgs args) {
      var cell = Field.Grid.GetGridObject(args.Position);
      
      cell.Placed = args.Building;
    }
  }
}