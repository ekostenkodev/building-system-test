using System;
using UniRx;
using UnityEngine;

namespace Kadoy.BuildingSystem.Render {
  public class BuildingRender : MonoBehaviour {
    [SerializeField]
    private Transform root;

    [SerializeField]
    private Renderer view;
    
    private readonly ISubject<Unit> interactived = new Subject<Unit>(); 
    private readonly ISubject<Unit> disabled = new Subject<Unit>();

    public Transform Root => root;
    public Renderer View => view;
    
    public IObservable<Unit> Interactived => interactived; 
    public IObservable<Unit> Disabled => disabled; 
    
    private void OnDisable() {
      disabled.OnNext(Unit.Default);
    }
    
    public void Interactive() {
      interactived.OnNext(Unit.Default);
    }
  }
}