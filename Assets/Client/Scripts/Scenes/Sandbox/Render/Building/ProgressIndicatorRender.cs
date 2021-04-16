using UniRx;
using UnityEngine;

namespace Kadoy.BuildingSystem.Render {
  public class ProgressIndicatorRender : MonoBehaviour {
    private const string MaterialFillKey = "_Fill";

    [SerializeField]
    private MeshRenderer indicator;

    private Transform cameraRoot;
    private Transform indicatorRoot;
    private CompositeDisposable disposables;
    private MaterialPropertyBlock materialBlock;

    private void Awake() {
      cameraRoot = Camera.main.transform;
      indicatorRoot = indicator.GetComponent<Transform>();
      disposables = new CompositeDisposable();
      materialBlock = new MaterialPropertyBlock();
      materialBlock.SetFloat(MaterialFillKey, 1);
    }

    private void OnDisable() {
      disposables.Clear();
    }

    private void Update() {
      RotateToCamera();
    }

    public void Progress(float current, float max) {
      materialBlock.SetFloat(MaterialFillKey, current / max);
      indicator.SetPropertyBlock(materialBlock);
    }

    private void RotateToCamera() {
      var forward = (transform.position - cameraRoot.position).normalized;
      var up = Vector3.Cross(forward, cameraRoot.right);

      indicatorRoot.rotation = Quaternion.LookRotation(forward, up);
    }
  }
}