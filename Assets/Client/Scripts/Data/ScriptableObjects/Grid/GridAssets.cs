using UnityEngine;

namespace Kadoy.BuildingSystem.Data.Scriptable {
  [CreateAssetMenu(fileName = "Grid/Asset")]
  public class GridAssets : ScriptableObject {
    [SerializeField]
    private int width;
    
    [SerializeField]
    private int height;
    
    [SerializeField]
    private int cellSize;

    public int Width => width;
    public int Height => height;
    public int CellSize => cellSize;
  }
}