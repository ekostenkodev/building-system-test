using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Kadoy.BuildingSystem.Sandbox.Controllers {
  public class CameraMovement : MonoBehaviour {
    private const float MovementSpeed = 20f;

    [SerializeField]
    private Camera camera;
    
    [SerializeField]
    private Transform root;

    private Vector3 dragOrigin;

    private void Update() {
      if (Input.GetMouseButtonDown(0)) {
        dragOrigin = Input.mousePosition;
      }

      if (Input.GetMouseButton(0) && !IsPointerOverUIObject()) {
        var position = camera.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        var speed = MovementSpeed * Time.deltaTime;
        var move = new Vector3(-position.x * speed, 0, -position.y * speed);

        root.position += move;
      }
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