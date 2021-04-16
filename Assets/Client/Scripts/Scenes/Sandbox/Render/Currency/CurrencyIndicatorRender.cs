using DG.Tweening;
using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Data.Scriptable;
using UnityEngine;
using UnityEngine.UI;

namespace Kadoy.BuildingSystem.Render {
  public class CurrencyIndicatorRender : MonoBehaviour {
    private const string CurrencyFormat = "{0} / {1}";
    private const float RecountDuration = 1f;

    [SerializeField]
    private CurrencyType type;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Text text;

    private int lastCurrent;
    private int lastMax;

    public CurrencyType Type => type;
    
    public void Initialize(ShopAssets shopAssets) {
      image.sprite = shopAssets.Icons.GetCurrencyIcon(Type);
    }

    public void Refresh(int current, int max) {
      var tempCurrent = lastCurrent;
      var tempMax = lastMax;

      DOTween
        .To(() => tempCurrent, v => tempCurrent = v, current, RecountDuration);
      DOTween
        .To(() => tempMax, v => tempMax = v, max, RecountDuration)
        .OnUpdate(() => text.text = string.Format(CurrencyFormat, tempCurrent, tempMax))
        .OnComplete(() => text.text = string.Format(CurrencyFormat, current, max));

      lastCurrent = current;
      lastMax = max;
    }
  }
}