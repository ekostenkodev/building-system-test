using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Kadoy.Util {
  public static class RichLinq {
    public static void ForEach<T>(this IList<T> source, Action<T> action) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      if (action == null) {
        throw new ArgumentNullException(nameof(action));
      }

      for (var i = 0; i < source.Count; i++) {
        action(source[i]);
      }
    }
    
    public static void ForEach<T>(this IList<T> source, Action<int, T> action) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      if (action == null) {
        throw new ArgumentNullException(nameof(action));
      }

      for (var i = 0; i < source.Count; i++) {
        action(i, source[i]);
      }
    }

    public static IList<T> Where<T>(this IList<T> source, Func<T, bool> predicate) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      if (predicate == null) {
        throw new ArgumentNullException(nameof(predicate));
      }

      var results = new List<T>();

      for (var i = 0; i < source.Count; i++) {
        var item = source[i];

        if (predicate(item)) {
          results.Add(item);
        }
      }

      return results;
    }

    public static T Aggregate<T>(this IList<T> source, T seed, Func<T, T, T> accumulator) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      if (accumulator == null) {
        throw new ArgumentNullException(nameof(accumulator));
      }

      var value = seed;

      for (var i = 0; i < source.Count; i++) {
        value = accumulator(value, source[i]);
      }

      return value;
    }

    public static bool All<T>(this IList<T> source, Func<T, bool> predicate) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      if (predicate == null) {
        throw new ArgumentNullException(nameof(predicate));
      }

      for (var i = 0; i < source.Count; i++) {
        if (!predicate(source[i])) {
          return false;
        }
      }

      return true;
    }

    public static bool Any<T>(this IList<T> source, Func<T, bool> predicate) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      if (predicate == null) {
        throw new ArgumentNullException(nameof(predicate));
      }

      for (var i = 0; i < source.Count; i++) {
        if (predicate(source[i])) {
          return true;
        }
      }

      return false;
    }

    public static IList<T> Distinct<T>(this IList<T> source) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      var result = new List<T>();

      for (var i = 0; i < source.Count; i++) {
        var item = source[i];

        if (!result.Contains(item)) {
          result.Add(item);
        }
      }

      return result;
    }

    public static IList<T> Except<T>(this IList<T> source, IList<T> second) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      if (second == null) {
        throw new ArgumentNullException(nameof(second));
      }

      var results = new List<T>();
      var hashSet = second.ToHashSet();

      for (var i = 0; i < source.Count; i++) {
        var item = source[i];

        if (!hashSet.Contains(item)) {
          results.Add(item);
        }
      }

      return results;
    }

    public static T FirstOrDefault<T>(this IList<T> source, Func<T, bool> predicate) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      if (predicate == null) {
        throw new ArgumentNullException(nameof(predicate));
      }

      for (var i = 0; i < source.Count; i++) {
        if (predicate(source[i])) {
          return source[i];
        }
      }

      return default;
    }

    public static T FirstOrDefault<T>(this IList<T> source) {
      return source.Count > 0 ? source[0] : default;
    }

    public static int FindIndex<T>(this IList<T> source, Func<T, bool> predicate) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      if (predicate == null) {
        throw new ArgumentNullException(nameof(predicate));
      }

      for (var i = 0; i < source.Count; i++) {
        if (predicate(source[i])) {
          return i;
        }
      }

      return -1;
    }

    public static T Find<T>(this IList<T> source, Func<T, bool> predicate) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      if (predicate == null) {
        throw new ArgumentNullException(nameof(predicate));
      }

      for (var i = 0; i < source.Count; i++) {
        if (predicate(source[i])) {
          return source[i];
        }
      }

      return default;
    }

    public static IList<Result> Select<T, Result>(this IList<T> source, Func<T, Result> predicate) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      if (predicate == null) {
        throw new ArgumentNullException(nameof(predicate));
      }

      var results = new List<Result>(source.Count);

      for (var i = 0; i < source.Count; i++) {
        results.Add(predicate(source[i]));
      }

      return results;
    }

    public static int Count<T>(this IList<T> source, Func<T, bool> predicate) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      if (predicate == null) {
        throw new ArgumentNullException(nameof(predicate));
      }

      var count = 0;

      for (var i = 0; i < source.Count; i++) {
        if (predicate(source[i])) {
          count++;
        }
      }

      return count;
    }

    public static List<T> ToList<T>(this IList<T> source) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      var list = new List<T>(source.Count);

      for (var i = 0; i < source.Count; i++) {
        list.Add(source[i]);
      }

      return list;
    }

    public static T[] ToArray<T>(this IList<T> source) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      var array = new T[source.Count];

      for (var i = 0; i < source.Count; i++) {
        array[i] = source[i];
      }

      return array;
    }

    public static Dictionary<Key, T> ToDictionary<T, Key>(this IList<T> source, Func<T, Key> predicate) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      if (predicate == null) {
        throw new ArgumentNullException(nameof(predicate));
      }

      var dictionary = new Dictionary<Key, T>(source.Count);

      for (var i = 0; i < source.Count; i++) {
        var item = source[i];
        dictionary.Add(predicate(item), item);
      }

      return dictionary;
    }

    public static HashSet<T> ToHashSet<T>(this IList<T> source) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      var hashSet = new HashSet<T>();

      for (var i = 0; i < source.Count; i++) {
        hashSet.Add(source[i]);
      }

      return hashSet;
    }

    public static List<T> Repeat<T>(T source, int count) {
      if (source == null) {
        throw new ArgumentNullException(nameof(source));
      }

      var buffer = new List<T>();

      for (var i = 0; i < count; i++) {
        buffer.Add(source);
      }

      return buffer;
    }

    public static T RandomItem<T>(this IList<T> source) {
      if (source.IsNullOrEmpty()) {
        return default;
      }

      var index = Random.Range(0, source.Count);

      return source[index];
    }
  }
}