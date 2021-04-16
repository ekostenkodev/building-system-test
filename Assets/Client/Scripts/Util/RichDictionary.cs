using System.Collections.Generic;
using UnityEngine;

namespace Kadoy.Util {
  public static class RichDictionary {
    public static bool ContainsKeySafe<T, V>(this Dictionary<T, V> dictionary, T key) {
      return null != dictionary && dictionary.ContainsKey(key);
    }

    public static V GetSafe<T, V>(this Dictionary<T, V> dictionary, T key) {
      if (null == key) {
        return default(V);
      }

      if (null != dictionary && dictionary.ContainsKeySafe(key)) {
        return dictionary[key];
      }

      return default(V);
    }
    
    public static V GetOrCreate<T, V>(this Dictionary<T, V> dictionary, T key) where V : new() {
      if (null == key || null == dictionary) {
        return default(V);
      }

      if (dictionary.ContainsKeySafe(key)) {
        return dictionary[key];
      }

      dictionary[key] = new V();

      return dictionary[key];
    }
  }
}