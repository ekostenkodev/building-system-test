using System;
using System.Collections.Generic;
using DG.Tweening;
using Kadoy.BuildingSystem.Render;
using Kadoy.Utils;
using UnityEngine;

namespace Kadoy.BuildingSystem.Sandbox.Controllers {
  public class ProgressConstructor {
    private readonly ComponentPool<ProgressIndicatorRender> pool;
    private readonly Dictionary<GameObject, ProgressArgs> progressMap;

    private ProgressIndicatorRender prefab;

    public ProgressConstructor(ProgressIndicatorRender indicator) {
      progressMap = new Dictionary<GameObject, ProgressArgs>();
      pool = new ComponentPool<ProgressIndicatorRender>(indicator);
    }

    public void Progress(GameObject root, TimeSpan duration) {
      if (progressMap.ContainsKey(root)) {
        Stop(root);
      }

      var indicator = pool.Rent();
      var indicatorRoot = indicator.transform;
      var delay = (float) duration.TotalSeconds;
      var tweener = DOTween
        .To(() => 0f, value => indicator.Progress(value, 1f), 1f, delay);

      indicatorRoot.parent = root.transform;
      indicatorRoot.localPosition = Vector3.zero;
      
      progressMap[root] = new ProgressArgs(indicator, tweener);
    }

    public void Stop(GameObject root) {
      if (progressMap.TryGetValue(root, out var args)) {
        progressMap.Remove(root);
        pool.Return(args.Indicator);
        args.Tweener.Kill();
      }
    }
  }

  public struct ProgressArgs {
    public ProgressIndicatorRender Indicator { get; }
    public Tweener Tweener { get; }

    public ProgressArgs(ProgressIndicatorRender indicator, Tweener tweener) {
      Indicator = indicator;
      Tweener = tweener;
    }
  }
}