using System;
using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Data.Scriptable;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Kadoy.BuildingSystem.Render.Shop {
  public class ShopBuildingCellRender : MonoBehaviour {
    private const string TimeFormat = @"mm\:ss\.ff";
    
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text nameText;
    
    [SerializeField]
    private Text descriptionText;
    
    [SerializeField]
    private Text buildDurationText;

    [SerializeField]
    private Button takeButton;

    [Header("Cost")]
    [SerializeField]
    private Transform costRoot;
    
    [SerializeField]
    private ShopBuildingCellCostRender costPrefab;

    private readonly ISubject<Unit> clicked = new Subject<Unit>();
    
    public IObservable<Unit> Clicked => clicked;

    private void Start() {
      takeButton
        .OnClickAsObservable()
        .Select(_ => Unit.Default)
        .Subscribe(clicked)
        .AddTo(this);
    }

    public void Initialize(BuildingModel model, string description, ShopAssets assets) {
      nameText.text = assets.Texts.GetBuildingName(model.Id);
      icon.sprite = assets.Icons.GetBuildingIcon(model.Id);
      descriptionText.text = description;
      buildDurationText.text = TimeSpan.FromMilliseconds(model.Сonditions.BuildDuration).ToString(TimeFormat);

      foreach (var currencyCost in model.Сonditions.Cost) {
        var costCell = Instantiate(costPrefab, costRoot);
        
        var cost = currencyCost.Count.ToString();
        var sprite = assets.Icons.GetCurrencyIcon(currencyCost.Type);

        costCell.Initialize(cost, sprite);
      }
    }
  }
}