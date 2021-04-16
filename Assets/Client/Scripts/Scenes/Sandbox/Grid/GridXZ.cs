using System;
using Kadoy.Util;
using UnityEngine;

namespace Kadoy.BuildingSystem.Grid {
  public class GridXZ<T> {
    private readonly int width;
    private readonly int height;
    private readonly float cellSize;
    private readonly Vector3 cellCenter;
    private readonly Vector3 originPosition;
    private readonly T[,] gridArray;

    public GridXZ(int width, int height, float cellSize, Vector3 originPosition,
      Func<int, int, T> createGridObject) {
      this.width = width;
      this.height = height;
      this.cellSize = cellSize;
      this.originPosition = originPosition;

      cellCenter = Vector3.one.WithY(0) * cellSize * 0.5f;
      gridArray = new T[width, height];

      for (int x = 0; x < gridArray.GetLength(0); x++) {
        for (int z = 0; z < gridArray.GetLength(1); z++) {
          gridArray[x, z] = createGridObject(x, z);
        }
      }

      var showDebug = true;
      if (showDebug) {
        for (int x = 0; x < gridArray.GetLength(0); x++) {
          for (int z = 0; z < gridArray.GetLength(1); z++) {
            Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);
          }
        }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
      }
    }
    
    public Vector3 GetCellCenter(int x, int z) {
      return GetWorldPosition(x, z) + cellCenter;
    }

    public T GetGridObject(Vector3 worldPosition) {
      var validatedPosition = GetXZ(worldPosition);
      return GetGridObject(validatedPosition.x, validatedPosition.y);
    }

    private Vector3 GetWorldPosition(int x, int z) {
      return new Vector3(x, 0, z) * cellSize + originPosition;
    }

    private Vector2Int GetXZ(Vector3 worldPosition) {
      var x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
      var z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);

      return new Vector2Int(x, z);
    }

    private T GetGridObject(int x, int z) {
      if (x >= 0 && z >= 0 && x < width && z < height) {
        return gridArray[x, z];
      } else {
        return default(T);
      }
    }
  }
}