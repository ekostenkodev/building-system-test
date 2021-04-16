using System;
using System.Collections.Generic;
using Kadoy.BuildingSystem.Render;
using Kadoy.Util;
using UnityEngine;

namespace Kadoy.BuildingSystem.Data.Scriptable {
  [CreateAssetMenu(menuName = "Building/BuildingAssets")]
  public class BuildingAssets : ScriptableObject {
    [SerializeField]
    private List<BuildingScriptableView> buildings;

    [SerializeField]
    private ProgressIndicatorRender progressIndicator;

    public ProgressIndicatorRender ProgressIndicator => progressIndicator;
    public BuildingRender FindBuilding(string id) => buildings.FirstOrDefault(building => building.Id == id)?.Prefab;
  }
  
  [Serializable]
  public class BuildingScriptableView {
    [SerializeField]
    private string id;

    [SerializeField]
    private BuildingRender prefab;

    public string Id => id;
    public BuildingRender Prefab => prefab;
  }
}