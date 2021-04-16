using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kadoy.Util {
  public static class RichTransform {
    public static void LocalReset(this Transform t) {
      t.localPosition = Vector3.zero;
      t.localRotation = Quaternion.identity;
      t.localScale = Vector3.one;
    }
    
    public static List<GameObject> GetChildren(this Transform t) {
      var list = new List<GameObject>();

      if (null == t) {
        return list;
      }

      foreach (Transform cell in t) {
        list.Add(cell.gameObject);
      }

      return list;
    }

    public static IReadOnlyCollection<GameObject> Flatten(this GameObject gameObject) {
      var buffer = new List<GameObject>();
      var children = gameObject.transform.GetChildren();

      buffer.AddRange(children);
      buffer.AddRange(children.SelectMany(Flatten).ToList());

      return buffer;
    }
    
    public static Transform Clear(this Transform t, bool immediate = false) {
      if (null == t || t.childCount <= 0) {
        return t;
      }

      if (immediate) {
        t.Cast<Transform>().ToList().ForEach(c => {
          Object.DestroyImmediate(c.gameObject, true);
        });
      } else {
        foreach (Transform c in t) {
          Object.Destroy(c.gameObject);
        }
      }

      return t;
    }
  }
}