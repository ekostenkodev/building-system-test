using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Kadoy.Util {
  public class RichRandom {
    public static int Pick(IEnumerable<int> probabilities) {
      var order = probabilities.OrderBy(x => x).ToArray();
      var total = order.Sum(x => x);
      var random = Random.Range(0, total + 1);
      var index = 0;

      foreach (var weight in order) {
        if (random <= weight) {
          break;
        }

        random -= weight;
        index++;
      }

      return index;
    }
    
    public static bool Check(int probability) {
      if (probability < 0 || probability > 100) {
        throw new Exception("Wrong probability");
      }
      
      return probability >= Random.Range(0, 100);
    }
  }
}