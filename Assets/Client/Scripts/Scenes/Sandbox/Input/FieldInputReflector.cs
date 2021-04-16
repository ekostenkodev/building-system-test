using System;
using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Data.Scriptable;
using Kadoy.BuildingSystem.Grid;
using Kadoy.BuildingSystem.Render;
using UnityEngine;

namespace Kadoy.BuildingSystem.Sandbox.Inputs {
  public class FieldInputReflector : MonoBehaviour, IDisposable {
    [SerializeField]
    private FieldInput input;

    [SerializeField]
    private GhostControllRender ghostControll;

    private BuildingModel model;
    private GridXZ<BuildingGridCell> grid;
    
    public event Action<PlaceArgs> Clicked;
    public event Action Closed;

    private void OnEnable() {
      input.Down += OnClick;
      input.Closed += OnClose;
      input.Moved += OnMove;
    }

    private void OnMove(Vector3 position) {
      var cell = grid.GetGridObject(position);
      var isOnField = cell != null;

      ghostControll.gameObject.SetActive(isOnField);

      if (!isOnField) {
        return;
      }
      
      var validatedPosition = grid.GetCellCenter(cell.X, cell.Y);
      var isAvailable = cell.IsEmpty;
      
      ghostControll.Refresh(validatedPosition, isAvailable);
    }

    private void OnDisable() {
      input.Down -= OnClick;
      input.Closed -= OnClose;
      input.Moved -= OnMove;
    }

    public void Initialize(GridXZ<BuildingGridCell> grid, BuildingAssets buildingAssets) {
      this.grid = grid;

      ghostControll.Initialize(buildingAssets);
    }

    public void Dispose() {
      ghostControll.Dispose();
      model = null;
    }

    private void OnClick(Vector3 worldPosition) {
      Clicked?.Invoke(new PlaceArgs(model, worldPosition));
    }
    
    private void OnClose() {
      Closed?.Invoke();
    }

    public void Render(BuildingModel model) {
      Dispose();
      
      this.model = model;
      
      ghostControll.Render(model);
    }
  }
}