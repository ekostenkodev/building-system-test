using Kadoy.BuildingSystem.Sandbox.Controllers;
using Kadoy.BuildingSystem.Services;
using UnityEngine;

namespace Kadoy.BuildingSystem.Sandbox {
  public class SandboxContext : MonoBehaviour {
    [SerializeField]
    private SandboxDataBuilder dataBuilder;

    [Header("Controllers")]
    [SerializeField]
    private FieldConstructorController fieldContainerConstructor;

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
      placeSelection.Inject(shop, fieldContainerConstructor, dataBuilder.BuildingAssets);
      buildingConstructor.Inject(db, shop, buildingService, fieldContainerConstructor, dataBuilder.BuildingAssets);
      fieldContainerConstructor.Inject(buildingConstructor, dataBuilder.GridAssets);
      currency.Inject(db, dataBuilder.ShopAsset);
    }

    private void Initialize() {
      fieldContainerConstructor.Initialize();
      shop.Initialize();
      placeSelection.Initialize();
      buildingConstructor.Initialize();
      currency.Initialize();
    }
  }
}