using System;
using Kadoy.BuildingSystem.Buildings.Goal;
using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Render;
using Kadoy.BuildingSystem.Sandbox;
using Kadoy.BuildingSystem.Sandbox.Controllers;
using Kadoy.BuildingSystem.Services;
using UniRx;
using UnityEngine;

namespace Kadoy.BuildingSystem.Buildings {
  public class GoalConstructor : IBuildingVisitor {
    private readonly BuildingRender building;
    private readonly Db db;
    private readonly IBuildingService buildingService;
    private readonly ProgressConstructor progressConstructor;

    public GoalConstructor(BuildingRender building, Db db, IBuildingService buildingService,
      ProgressConstructor progressConstructor) {
      this.building = building;
      this.db = db;
      this.buildingService = buildingService;
      this.progressConstructor = progressConstructor;
    }

    public void Visit(MiningBuildingModel model) {
      var goal = new MiningGoal(model.Mining, db, buildingService);
      var progressRoot = building.Root.gameObject;
      var progressDuration = TimeSpan.FromMilliseconds(model.Mining.Delay);

      goal.Mining
        .Where(isActive => isActive)
        .Subscribe(_ => progressConstructor.Progress(progressRoot, progressDuration))
        .AddTo(building);

      building.Disabled
        .Subscribe(_ => goal.Dispose())
        .AddTo(building);

      building.Interactived
        .Subscribe(_ => goal.Execute())
        .AddTo(building);

      goal.Start();
    }

    public void Visit(StorageBuildingModel model) {
      var goal = new StorageGoal(model.Storage, db, buildingService);

      goal.Execute();
    }
  }
}