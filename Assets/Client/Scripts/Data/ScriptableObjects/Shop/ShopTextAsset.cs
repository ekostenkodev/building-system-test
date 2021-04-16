using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kadoy.BuildingSystem.Data.Scriptable {
  [CreateAssetMenu(menuName = "Shop/Assets/Texts")]
  public class ShopTextAsset : ScriptableObject {
    [SerializeField]
    private List<Building> buildings;
    
    public string GetBuildingName(string id) => buildings.FirstOrDefault(icon => icon.Id == id)?.Name;
    public string GetBuildingDescription(string id) => buildings.FirstOrDefault(icon => id.Contains(icon.Id))?.Description;
    
    [Serializable]
    public class Building {
      [SerializeField]
      private string id;
      
      [SerializeField]
      private string name;
      
      [SerializeField]
      private string description;

      public string Id => id;
      public string Name => name;
      public string Description => description;
    }
  }
}