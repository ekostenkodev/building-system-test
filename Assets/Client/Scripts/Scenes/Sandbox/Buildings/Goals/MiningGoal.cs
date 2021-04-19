using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Sandbox;
using Kadoy.BuildingSystem.Services;
using UniRx;

namespace Kadoy.BuildingSystem.Buildings.Goal {
  public class MiningGoal : BuildingGoal, IDisposable {
    private readonly MiningModel model;
    private readonly Db db;
    private readonly IBuildingService service;

    private bool isAvailableToCollect;
    private CancellationTokenSource cancellationTokenSource;

    public ReactiveProperty<bool> Mining { get; }

    public MiningGoal(MiningModel model, Db db, IBuildingService service) {
      this.model = model;
      this.db = db;
      this.service = service;

      Mining = new ReactiveProperty<bool>();
    }

    public override void Start() {
      MiningForResult();
    }

    public override void Execute() {
      if (isAvailableToCollect) {
        TakeMiningResult();
        MiningForResult();
      }
    }

    private void TakeMiningResult() {
      isAvailableToCollect = false;

      var user = service.Mining(model);

      db.UpdateUser(user);
    }

    private UniTask MiningForResult() {
      return MiningForResultAsync();
    }

    private async UniTask MiningForResultAsync() {
      Dispose();

      Mining.Value = true;
      cancellationTokenSource = new CancellationTokenSource();

      var delay = TimeSpan.FromMilliseconds(model.Delay);
      var token = cancellationTokenSource.Token;

      await UniTask.Delay(delay, cancellationToken: token);

      Mining.Value = false;
      isAvailableToCollect = true;
      
      Dispose();
    }

    public void Dispose() {
      cancellationTokenSource?.Cancel(false);
    }
  }
}