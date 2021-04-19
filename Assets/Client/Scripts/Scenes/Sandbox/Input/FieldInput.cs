using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Kadoy.BuildingSystem.Sandbox.Inputs {
  public class FieldInput : MonoBehaviour {
    private const float MaxDistance = 1000f;

    [SerializeField]
    private new Camera camera;

    [SerializeField]
    private LayerMask mouseColliderLayerMask;

    private Vector3 lastPosition;

    public event Action<Vector3> Down;
    public event Action<Vector3> Moved;
    public event Action Closed;

    private void Update() {
      var inputPosition = Input.mousePosition;

      CheckMove(inputPosition);
      CheckClick(inputPosition);
      CheckClose();
    }

    private void CheckClose() {
      if (Input.GetMouseButtonDown(1)) {
        Closed?.Invoke();
      }
    }

    private void CheckClick(Vector3 inputPosition) {
      if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject()) {
        if (TryColliding(inputPosition, out var collidePosition)) {
          Down?.Invoke(collidePosition);
        }
      }
    }

    private void CheckMove(Vector3 inputPosition) {
      if (inputPosition != lastPosition) {
        if (TryColliding(inputPosition, out var collidePosition)) {
          Moved?.Invoke(collidePosition);
        } else {
          Moved?.Invoke(Vector3.negativeInfinity);
        }

        lastPosition = inputPosition;
      }
    }

    private bool TryColliding(Vector3 inputPosition, out Vector3 collidePosition) {
      var ray = camera.ScreenPointToRay(inputPosition);
      var isContact = Physics.Raycast(ray, out var raycastHit, MaxDistance, mouseColliderLayerMask);

      collidePosition = raycastHit.point;

      return isContact;
    }

    private bool IsPointerOverUIObject() {
      var eventDataCurrentPosition =
        new PointerEventData(EventSystem.current) {
          position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
      var results = new List<RaycastResult>();
      EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
      return results.Count > 0;
    }
  }
}