using System;
using System.Collections.Generic;
using System.Linq;
using Kadoy.BuildingSystem.Data.Model;
using UnityEngine;

namespace Kadoy.BuildingSystem.Data.Scriptable {
  [CreateAssetMenu(menuName = "Shop/Assets/Icons")]
  public class ShopIconAsset : ScriptableObject {
    [SerializeField]
    private List<BuildingIcon> buildings;
    
    [SerializeField]
    private List<CurrencyIcon> currency;

    public Sprite GetBuildingIcon(string id) => buildings.FirstOrDefault(icon => icon.Id == id)?.Sprite;
    public Sprite GetCurrencyIcon(CurrencyType type) => currency.FirstOrDefault(icon => icon.Type == type)?.Sprite;

    [Serializable]
    public class BuildingIcon {
      [SerializeField]
      private string id;
      
      [SerializeField]
      private Sprite sprite;

      public string Id => id;
      public Sprite Sprite => sprite;
    }
    
    [Serializable]
    public class CurrencyIcon {
      [SerializeField]
      private CurrencyType type;
      
      [SerializeField]
      private Sprite sprite;

      public CurrencyType Type => type;
      public Sprite Sprite => sprite;
    }
  }
}