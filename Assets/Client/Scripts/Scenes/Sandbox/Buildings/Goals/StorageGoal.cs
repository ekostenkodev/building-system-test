using Cysharp.Threading.Tasks;
using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Sandbox;
using Kadoy.BuildingSystem.Services;

namespace Kadoy.BuildingSystem.Buildings.Goal {
  public class StorageGoal : BuildingGoal {
    private readonly StorageModel model;
    private readonly Db db;
    private readonly IBuildingService service;

    public StorageGoal(StorageModel model, Db db, IBuildingService service) {
      this.model = model;
      this.db = db;
      this.service = service;
    }

    public override void Execute() {
      var user = service.Storage(model);

      db.UpdateUser(user);
    }
  }
}