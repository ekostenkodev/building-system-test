using Kadoy.BuildingSystem.Data.Model;
using Kadoy.BuildingSystem.Sandbox;

namespace Kadoy.BuildingSystem.Services {
  internal partial class ClientServiceImpl {
    private readonly Db db;
    private UserModel User => db.User.Value;
    
    public ClientServiceImpl(Db db) {
      this.db = db;
    }
  }
}