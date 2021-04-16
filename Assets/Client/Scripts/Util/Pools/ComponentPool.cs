using UniRx.Toolkit;
using UnityEngine;

namespace Kadoy.Utils {
  public class ComponentPool<T> : ObjectPool<T> where T : Component{
    private readonly T prefab;
    private readonly Transform parent;

    public ComponentPool(T prefab, Transform parent = null) {
      this.prefab = prefab;
      this.parent = parent;
    }

    protected override T CreateInstance() {
      var instance = Object.Instantiate(prefab, parent);
      
      instance.name = prefab.name;
      
      return instance;
    }
  }
}