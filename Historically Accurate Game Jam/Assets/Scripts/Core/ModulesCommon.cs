using Hub;
using Player;
using Upgrades;

namespace Core
{
  public static class ModulesCommon
  {
    public static ModulePlayer   ModulePlayer  { get; set; }  = new ModulePlayer();
    public static ModuleUpgrades ModuleUpgrade { get; set; } = new ModuleUpgrades();
    public static ModuleSales    ModuleSales   { get; set; } = new ModuleSales();
    public static SceneLoader    SceneLoader   { get; set; }

    public static void loadNextScene()
    {
      SceneLoader.loadNextScene();
    }
  }
}