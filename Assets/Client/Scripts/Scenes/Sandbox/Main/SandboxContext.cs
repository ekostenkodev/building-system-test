using Kadoy.BuildingSystem.Sandbox.Controllers;
using Kadoy.BuildingSystem.Services;
using UnityEngine;

namespace Kadoy.BuildingSystem.Sandbox {
  public class SandboxContext : MonoBehaviour {
    [SerializeField]
    private SandboxDataBuilder dataBuilder;

    [Header("Controllers")]
    [SerializeField]
    private FielderConstructorController fielderConstructor;

    [SerializeField]
    private PlaceSelectionController placeSelection;

    [SerializeField]
    private BuildingConstructorController buildingConstructor;

    [SerializeField]
    private ShopController shop;

    [SerializeField]
    private CurrencyController currency;

    private Db db;
    private IShopService shopService;
    private IBuildingService buildingService;

    public void Awake() {
      db = dataBuilder.Create();
      shopService = new ClientServiceImpl(db);
      buildingService = new ClientServiceImpl(db);

      InjectDependencies();
      Initialize();
    }

    private void InjectDependencies() {
      shop.Inject(db, shopService, placeSelection, buildingConstructor, dataBuilder.ShopAsset);
      placeSelection.Inject(shop, fielderConstructor, dataBuilder.BuildingAssets);
      buildingConstructor.Inject(db, shop, buildingService, fielderConstructor, dataBuilder.BuildingAssets);
      fielderConstructor.Inject(buildingConstructor, dataBuilder.GridAssets);
      currency.Inject(db, dataBuilder.ShopAsset);
    }

    private void Initialize() {
      fielderConstructor.Initialize();
      shop.Initialize();
      placeSelection.Initialize();
      buildingConstructor.Initialize();
      currency.Initialize();
    }
  }
}