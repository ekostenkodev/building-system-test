using System;
using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Data.Scriptable;
using Kadoy.BuildingSystem.Sandbox.Inputs;
using UnityEngine;

namespace Kadoy.BuildingSystem.Sandbox.Controllers {
  public class PlaceSelectionController : MonoBehaviour, IPlaceSelector {
    [SerializeField]
    private FieldInputReflector inputReflector;

    private IBuildingSelector selector;
    private IFieldContainer fieldContainer;
    private BuildingAssets buildingAssets;
    
    public event Action<PlaceArgs> Placed;

    private void OnDisable() {
      if (selector != null) {
        selector.Selected -= OnStartBuilding;
      }
      
      inputReflector.Clicked -= OnClicked;
      inputReflector.Closed -= OnInputClose;
    }

    public void Inject(IBuildingSelector selector, IFieldContainer fieldContainer, BuildingAssets buildingAssets) {
      this.selector = selector;
      this.fieldContainer = fieldContainer;
      this.buildingAssets = buildingAssets;
    }

    public void Initialize() {
      inputReflector.Initialize(fieldContainer.Field.Grid, buildingAssets);

      selector.Selected += OnStartBuilding;
      inputReflector.Clicked += OnClicked;
      inputReflector.Closed += OnInputClose;
    }

    private void OnInputClose() {
      inputReflector.Dispose();
      inputReflector.gameObject.SetActive(false);
    }

    private void OnClicked(PlaceArgs args) {
      OnInputClose();
      
      var grid = fieldContainer.Field.Grid;
      var cell = grid.GetGridObject(args.Position);

      if (cell.IsEmpty) {
        var buildingPosition = grid.GetCellCenter(cell.X, cell.Y);

        Placed?.Invoke(new PlaceArgs(args.Building, buildingPosition));
      }
    }

    private void OnStartBuilding(BuildingModel building) {
      inputReflector.Render(building);
      inputReflector.gameObject.SetActive(true);
    }
  }
}