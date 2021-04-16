using System;
using System.Collections.Generic;
using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Data.Scriptable;
using UniRx;
using UnityEngine;

namespace Kadoy.BuildingSystem.Render.Shop {
  public class ShopBuildingsLayoutRender : MonoBehaviour, IDisposable {
    [Space]
    [SerializeField]
    private Transform root;

    [Space]
    [SerializeField]
    private ShopBuildingCellRender cellPrefab;

    private readonly List<ShopBuildingCellRender> cellsBuffer = new List<ShopBuildingCellRender>();
    private readonly CompositeDisposable disposables = new CompositeDisposable();
    private readonly ISubject<BuildingModel> clicked = new Subject<BuildingModel>();
    private ShopAssets shopAsset;
    private BuildingListModel data;

    public IObservable<BuildingModel> Clicked => clicked;

    private void OnDisable() {
      Dispose();
    }

    public void Initialize(ShopAssets shopAsset, BuildingListModel data) {
      this.shopAsset = shopAsset;
      this.data = data;
    }

    public void Render() {
      Dispose();
      CreateCells(data.Mining);
      CreateCells(data.Storage);
    }

    public void Dispose() {
      disposables.Clear();

      foreach (var cell in cellsBuffer) {
        Destroy(cell.gameObject);
      }

      cellsBuffer.Clear();
    }

    private void CreateCells(IEnumerable<MiningBuildingModel> models) {
      foreach (var model in models) {
        var descriptionFormat = shopAsset.Texts.GetBuildingDescription(model.Id);
        var delayInSeconds = TimeSpan.FromMilliseconds(model.Mining.Delay).TotalSeconds;
        var description = string.Format(descriptionFormat, model.Mining.Currency.Count, delayInSeconds);
        
        CreateCell(model, description);
      }
    }

    private void CreateCells(IEnumerable<StorageBuildingModel> models) {
      foreach (var model in models) {
        var descriptionFormat = shopAsset.Texts.GetBuildingDescription(model.Id);
        var description = string.Format(descriptionFormat, model.Storage.Currency.Count);
        
        CreateCell(model, description);
      }
    }
    
    private void CreateCell(BuildingModel model, string description) {
      var building = Instantiate(cellPrefab, root);

      building.Initialize(model, description, shopAsset);
      building.Clicked
        .Select(_ => model)
        .Subscribe(clicked)
        .AddTo(disposables);

      cellsBuffer.Add(building);
    }
  }
}