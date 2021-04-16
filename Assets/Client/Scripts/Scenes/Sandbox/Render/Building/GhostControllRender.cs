using DG.Tweening;
using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Data.Scriptable;
using Kadoy.BuildingSystem.Render;
using UnityEngine;

namespace Kadoy.BuildingSystem.Render {
  public class GhostControllRender : MonoBehaviour {
    private const float MoveDuration = 0.15f;

    [SerializeField]
    private Material correctMaterial;

    [SerializeField]
    private Material wrongMaterial;

    private Vector3 lastPosition;
    private Tweener moveTweener;
    private BuildingAssets buildingAssets;
    private BuildingRender ghost;

    private void OnDisable() {
      moveTweener?.Kill();
    }

    public void Initialize(BuildingAssets buildingAssets) {
      this.buildingAssets = buildingAssets;
    }

    public void Render(BuildingModel model) {
      var prefab = buildingAssets.FindBuilding(model.Id);

      ghost = Instantiate(prefab, transform);
      ghost.Root.position = lastPosition;
    }

    public void Dispose() {
      if (ghost != null) {
        Destroy(ghost.gameObject);
      }
    }

    public void Refresh(Vector3 position, bool isAvailable) {
      if (lastPosition == position) {
        return;
      }

      moveTweener?.Kill();
      moveTweener = ghost.Root.DOMove(position, MoveDuration);
      ghost.View.material = isAvailable ? correctMaterial : wrongMaterial;
      lastPosition = position;
    }
  }
}