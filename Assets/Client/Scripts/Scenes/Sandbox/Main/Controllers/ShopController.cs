using System;
using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Data.Scriptable;
using Kadoy.BuildingSystem.Render.Shop;
using Kadoy.BuildingSystem.Services;
using UniRx;
using UnityEngine;

namespace Kadoy.BuildingSystem.Sandbox.Controllers {
  public class ShopController : MonoBehaviour, IBuildingSelector, IBuildingAcquirer {
    [SerializeField]
    private ShopRender shop;

    [SerializeField]
    private ShopControlRender control;

    private bool isConstructing;
    private Db db;
    private IShopService service;
    private IPlaceSelector selector;
    private IBuildingConstructor constructor;
    private ShopAssets shopAssets;
    private CompositeDisposable disposables;

    public event Action<BuildingModel> Selected;
    public event Action<PlaceArgs> Acquired;

    private void OnDisable() {
      disposables?.Clear();

      control.Opened -= OpenShop;
      control.Closed -= CloseShop;
      shop.BuildingSelected -= OnBuildingSelected;

      selector.Placed -= OnPlaced;
      constructor.Constructed -= OnConstructed;
    }

    public void Inject(Db db,
      IShopService service,
      IPlaceSelector selector,
      IBuildingConstructor constructor,
      ShopAssets shopAssets) {
      this.db = db;
      this.selector = selector;
      this.service = service;
      this.constructor = constructor;
      this.shopAssets = shopAssets;
    }

    public void Initialize() {
      disposables?.Clear();
      disposables = new CompositeDisposable();

      shop.Initialize(db.Buildings.Value, shopAssets);

      control.Opened += OpenShop;
      control.Closed += CloseShop;
      shop.BuildingSelected += OnBuildingSelected;

      selector.Placed += OnPlaced;
      constructor.Constructed += OnConstructed;
    }

    private void OnPlaced(PlaceArgs args) {
      if (service.TryPurchaseBuilding(args.Building, out var user)) {
        db.UpdateUser(user);

        Acquired?.Invoke(args);

        isConstructing = true;
      }
    }

    private void OnConstructed(BuildingArgs args) {
      isConstructing = false;
    }

    private void OnBuildingSelected(BuildingModel building) {
      var isAvailable = !isConstructing && service.IsAvailableToBuy(building);

      if (isAvailable) {
        Selected?.Invoke(building);
        CloseShop();
      } 
    }

    private void OpenShop() {
      shop.Open();
      control.SetButtonsActive(false, true);
    }

    private void CloseShop() {
      shop.Close();
      control.SetButtonsActive(true, false);
    }
  }
}