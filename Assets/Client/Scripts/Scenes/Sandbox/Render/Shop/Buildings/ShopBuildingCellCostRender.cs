using UnityEngine;
using UnityEngine.UI;

namespace Kadoy.BuildingSystem.Render.Shop {
  public class ShopBuildingCellCostRender : MonoBehaviour {
    [SerializeField]
    private Text costText;

    [SerializeField]
    private Image costIcon;

    public void Initialize(string cost, Sprite sprite) {
      costText.text = cost;
      costIcon.sprite = sprite;
    }
  }
}