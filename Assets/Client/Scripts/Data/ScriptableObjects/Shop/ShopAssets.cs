using UnityEngine;

namespace Kadoy.BuildingSystem.Data.Scriptable {
  [CreateAssetMenu(menuName = "Shop/Shop Asset")]
  public class ShopAssets : ScriptableObject {
    [SerializeField]
    private ShopIconAsset icons;
    
    [SerializeField]
    private ShopTextAsset texts;

    public ShopIconAsset Icons => icons;
    public ShopTextAsset Texts => texts;
  }
}