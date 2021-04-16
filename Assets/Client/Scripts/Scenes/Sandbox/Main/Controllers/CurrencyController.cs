using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Data.Scriptable;
using Kadoy.BuildingSystem.Render;
using UniRx;
using UnityEngine;

namespace Kadoy.BuildingSystem.Sandbox.Controllers {
  public class CurrencyController : MonoBehaviour {
    [SerializeField]
    private CurrencyIndicatorsRender indicatorsRender;
    
    private Db db;
    private ShopAssets shopAssets;
    private CompositeDisposable disposables;

    private void OnDisable() {
      disposables?.Clear();
    }

    public void Inject(Db db, ShopAssets shopAssets) {
      this.db = db;
      this.shopAssets = shopAssets;

      disposables = new CompositeDisposable();
    }

    public void Initialize() {
      db.User.Subscribe(OnUserChange).AddTo(disposables);
      indicatorsRender.Initialize(shopAssets);
    }

    private void OnUserChange(UserModel user) {
      indicatorsRender.Refresh(user.Currency);
    }
  }
}