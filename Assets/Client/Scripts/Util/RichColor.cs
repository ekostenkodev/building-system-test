using UnityEngine;

namespace Kadoy.Util {
  public static class RichColor {
    public static Color ToColor(this string hexString) {
      ColorUtility.TryParseHtmlString(hexString, out var outColor);
      return outColor;
    }
    
    public static Color WithA(this Color color, float a) {
      color.a = a;

      return color;
    }
  }
}