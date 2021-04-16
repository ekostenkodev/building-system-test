using Cysharp.Threading.Tasks;

namespace Kadoy.BuildingSystem.Buildings.Goal {
  public abstract class BuildingGoal {
    public virtual void Start() { }
    public abstract void Execute();
  }
}