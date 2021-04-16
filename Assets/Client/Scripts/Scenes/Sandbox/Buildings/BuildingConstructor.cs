using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Data.Scriptable;
using Kadoy.BuildingSystem.Render;
using Kadoy.BuildingSystem.Sandbox.Controllers;
using UnityEngine;

namespace Kadoy.BuildingSystem.Sandbox {
  public class BuildingConstructor : IDisposable {
    private readonly Transform root;
    private readonly BuildingAssets buildingAssets;
    private readonly ProgressConstructor progressConstructor;
    private CancellationTokenSource cancellationTokenSource;

    public BuildingConstructor(Transform root, BuildingAssets buildingAssets, ProgressConstructor progressConstructor) {
      this.root = root;
      this.buildingAssets = buildingAssets;
      this.progressConstructor = progressConstructor;
    }

    public async UniTask<BuildingRender> Build(BuildingModel model, Vector3 position) {
      if (cancellationTokenSource != null) {
        return null;
      }

      Dispose();
      
      cancellationTokenSource = new CancellationTokenSource();

      var prefab = buildingAssets.FindBuilding(model.Id);
      var building = GameObject.Instantiate(prefab, root);

      var delay = TimeSpan.FromMilliseconds(model.Сonditions.BuildDuration);
      var token = cancellationTokenSource.Token;

      building.Root.position = position;

      progressConstructor.Progress(building.Root.gameObject, delay);

      await UniTask.Delay(delay, cancellationToken: token);

      cancellationTokenSource = null;
      progressConstructor.Stop(building.Root.gameObject);

      return building;
    }

    public void Dispose() {
      cancellationTokenSource?.Cancel();
    }
  }
}