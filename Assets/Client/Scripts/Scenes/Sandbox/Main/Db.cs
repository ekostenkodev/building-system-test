using Kadoy.BuildingSystem.Data.Model;
using UniRx;

namespace Kadoy.BuildingSystem.Sandbox {
  public class Db {
    public ReactiveProperty<UserModel> User { get; }
    public ReactiveProperty<BuildingListModel> Buildings { get; }

    public Db() {
      User = new ReactiveProperty<UserModel>();
      Buildings = new ReactiveProperty<BuildingListModel>();
    }

    public void UpdateUser(UserModel user) {
      User.Value = user;
    }
    
    public void UpdateBuildings(BuildingListModel buildings) {
      Buildings.Value = buildings;
    }
  }
}