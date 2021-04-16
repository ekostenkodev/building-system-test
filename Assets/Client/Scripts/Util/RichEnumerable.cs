using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Random = UnityEngine.Random;

namespace Kadoy.Util {
  public static class RichEnumerable {
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) {
      if (enumerable == null) {
        return true;
      }

      return !enumerable.Any();
    }

    public static bool IsNullOrEmpty<T>(this List<T> list) {
      return list == null || list.Count == 0;
    }

    public static bool IsNullOrEmpty<T>(this T[] array) {
      return array == null || array.Length == 0;
    }

    public static bool IsNullOrEmpty<T, K>(this Dictionary<T, K> dictionary) {
      return dictionary == null || dictionary.Count == 0;
    }

    public static bool IsNullOrEmpty(this string str) {
      return string.IsNullOrEmpty(str);
    }

    public static IEnumerable<T> NotNull<T>(this IEnumerable<T> enumerable) {
      return enumerable.Where(_ => _ != null);
    }

    public static int CountSafe<T>(this IEnumerable<T> enumerable) {
      return enumerable?.Count() ?? 0;
    }

    public static string AsString<T>(this IEnumerable<T> enumerable, string separator = "") {
      if (typeof(string) == typeof(T)) {
        var ex = (IEnumerable<string>) enumerable;

        return ex.IsNullOrEmpty() ? string.Empty : string.Join(separator, ex.Select(x => x).ToArray());
      }

      return enumerable.IsNullOrEmpty()
        ? string.Empty
        : string.Join(", ", enumerable.Select(x => x.ToString()).ToArray());
    }

    public static string AsString<T>(this IEnumerable<T> enumerable, Func<T, string> fn) {
      return enumerable.IsNullOrEmpty() ? string.Empty : string.Join(", ", enumerable.Select(fn).ToArray());
    }

    public static T RandomElement<T>(this IEnumerable<T> enumerable) {
      if (enumerable.IsNullOrEmpty()) {
        return default;
      }

      var index = Random.Range(0, enumerable.Count());

      return enumerable.ElementAt(index);
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
      if (source == null) throw new ArgumentNullException(nameof(source));
      if (action == null) throw new ArgumentNullException(nameof(action));

      foreach (var item in source) {
        action(item);
      }
    }
    
    public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action) {
      if (source == null) throw new ArgumentNullException(nameof(source));
      if (action == null) throw new ArgumentNullException(nameof(action));

      var index = 0;

      foreach (var item in source) {
        action(item, index);
        index++;
      }
    }

    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source) {
      var range = new System.Random();

      return source.Randomize(range);
    }

    private static IEnumerable<T> Randomize<T>(this IEnumerable<T> source, System.Random range) {
      if (source == null) throw new ArgumentNullException(nameof(source));
      if (range == null) throw new ArgumentNullException(nameof(range));

      return source.RandomizeAlgorithm(range);
    }

    private static IEnumerable<T> RandomizeAlgorithm<T>(this IEnumerable<T> source, System.Random range) {
      var temp = source.ToList();

      for (var i = 0; i < temp.Count; i++) {
        var j = range.Next(i, temp.Count);

        yield return temp[j];

        temp[j] = temp[i];
      }
    }

    public static void Shuffle<T>(this IList<T> list) {
      for (var i = list.Count; i > 0; i--)
        list.Swap(0, Random.Range(0, i));
    }

    private static void Swap<T>(this IList<T> list, int i, int j) {
      var temp = list[i];
      list[i] = list[j];
      list[j] = temp;
    }
  }
}