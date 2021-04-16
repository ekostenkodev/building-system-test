using System.Collections.Generic;
using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Data.Scriptable;
using Kadoy.Util;
using UnityEngine;

namespace Kadoy.BuildingSystem.Render {
  public class CurrencyIndicatorsRender : MonoBehaviour {
    [SerializeField]
    private List<CurrencyIndicatorRender> indicators;

    public void Initialize(ShopAssets shopAssets) {
      foreach (var indicator in indicators) {
        indicator.Initialize(shopAssets);
      }
    }

    public void Refresh(UserCurrency currency) { 
      foreach (var current in currency.Current) {
        var indicator = indicators.FirstOrDefault(i => i.Type == current.Type);
        var max = currency.Max.FirstOrDefault(x => x.Type == current.Type);

        indicator.Refresh(current.Count, max.Count);
      }
    }
  }
}