using System;
using Cysharp.Threading.Tasks;
using Kadoy.BuildingSystem.Buildings;
using Kadoy.BuildingSystem.Data.Scriptable;
using Kadoy.BuildingSystem.Sandbox.Inputs;
using Kadoy.BuildingSystem.Services;
using UnityEngine;

namespace Kadoy.BuildingSystem.Sandbox.Controllers {
  public class BuildingConstructorController : MonoBehaviour, IBuildingConstructor {
    [Space]
    [SerializeField]
    private Transform buildingRoot;

    [SerializeField]
    private FieldInput input;

    private Db db;
    private IBuildingAcquirer acquirer;
    private IBuildingService service;
    private IFieldContainer fieldContainer;
    private BuildingConstructor buildingConstructor;
    private ProgressConstructor progressConstructor;
    private BuildingAssets buildingAssets;

    public event Action<BuildingArgs> Constructed;

    private void OnDisable() {
      if (acquirer != null) {
        acquirer.Acquired -= OnAcquired;
      }

      input.Down -= OnClicked;

      buildingConstructor.Dispose();
    }

    public void Inject(Db db,
      IBuildingAcquirer acquirer,
      IBuildingService service,
      IFieldContainer fieldContainer,
      BuildingAssets buildingAssets) {
      this.db = db;
      this.acquirer = acquirer;
      this.service = service;
      this.fieldContainer = fieldContainer;
      this.buildingAssets = buildingAssets;
    }

    public void Initialize() {
      acquirer.Acquired += OnAcquired;
      input.Down += OnClicked;

      progressConstructor = new ProgressConstructor(buildingAssets.ProgressIndicator);
      buildingConstructor = new BuildingConstructor(buildingRoot, buildingAssets, progressConstructor);
    }

    private void OnAcquired(PlaceArgs args) {
      ConstructBuilding(args);
    }

    private UniTask ConstructBuilding(PlaceArgs args) {
      return ConstructBuildingAsync(args);
    }

    private async UniTask ConstructBuildingAsync(PlaceArgs args) {
      var building = await buildingConstructor.Build(args.Building, args.Position);

      if (building == null) {
        return;
      }

      var goal = new GoalConstructor(building, db, service, progressConstructor);

      args.Building.Accept(goal);

      Constructed?.Invoke(new BuildingArgs(building, args.Position));
    }

    private void OnClicked(Vector3 position) {
      var cell = fieldContainer.Field.Grid.GetGridObject(position);

      if (!cell.IsEmpty) {
        cell.Placed.Interactive();
      }
    }
  }
}