using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Kadoy.BuildingSystem.Render.Shop {
  public class ShopControlRender : MonoBehaviour {
    [SerializeField]
    private Button openButton;

    [SerializeField]
    private Button closeButton;
    
    public event Action Opened;
    public event Action Closed;
    
    private void Start() {
      openButton.OnClickAsObservable()
        .Subscribe(_ => Opened?.Invoke())
        .AddTo(this);

      closeButton.OnClickAsObservable()
        .Subscribe(_ => Closed?.Invoke())
        .AddTo(this);
    }

    public void SetButtonsActive(bool openActive = false, bool closeActive = false) {
      openButton.gameObject.SetActive(openActive);
      closeButton.gameObject.SetActive(closeActive);
    }
  }
}