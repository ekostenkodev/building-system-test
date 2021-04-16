using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Data.Scriptable;
using Newtonsoft.Json;
using UnityEngine;

namespace Kadoy.BuildingSystem.Sandbox {
  public class SandboxDataBuilder : MonoBehaviour {
    [Header("Scene data")]
    [SerializeField]
    private TextAsset userAsset;

    [SerializeField]
    private TextAsset buildingsAsset;
    
    [Header("Assets")]
    [SerializeField]
    private BuildingAssets buildingAssets;

    [SerializeField]
    private ShopAssets shopAsset;
    
    [SerializeField]
    private GridAssets gridAssets;

    public BuildingAssets BuildingAssets => buildingAssets;
    public ShopAssets ShopAsset => shopAsset;
    public GridAssets GridAssets => gridAssets;

    public Db Create() {
      var db = new Db();

      var userWrapper = Load<UserWrapper>(userAsset);
      var buildingsWrapper = Load<BuildingsWrapper>(buildingsAsset);
      
      db.UpdateUser(userWrapper.User);
      db.UpdateBuildings(buildingsWrapper.Buildings);

      return db;
    }
    
    private T Load<T>(TextAsset asset){
      var data = JsonConvert.DeserializeObject<T>(asset.text);
      
      return data;
    }
  }
}