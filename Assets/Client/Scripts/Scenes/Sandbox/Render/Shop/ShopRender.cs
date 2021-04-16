using System;
using System.Collections.Generic;
using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Data.Scriptable;
using UniRx;
using UnityEngine;

namespace Kadoy.BuildingSystem.Render.Shop {
  public class ShopRender : MonoBehaviour, IDisposable {
    [SerializeField]
    private ShopBuildingsLayoutRender buildingsLayout;

    private readonly CompositeDisposable disposables = new CompositeDisposable();

    public event Action<BuildingModel> BuildingSelected;

    public void Initialize(BuildingListModel data, ShopAssets shopAssets) {
      buildingsLayout.Initialize(shopAssets, data);
    }

    public void Open() {
      Dispose();

      buildingsLayout.Render();
      buildingsLayout.Clicked
        .Subscribe(building => BuildingSelected?.Invoke(building))
        .AddTo(disposables);

      buildingsLayout.gameObject.SetActive(true);
    }

    public void Close() {
      Dispose();

      buildingsLayout.gameObject.SetActive(false);
    }

    public void Dispose() {
      disposables.Clear();
      buildingsLayout.Dispose();
      buildingsLayout.gameObject.SetActive(false);
    }
  }
}