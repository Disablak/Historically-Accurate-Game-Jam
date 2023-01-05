using Player;
using Upgrades;

namespace Core
{
  public static class ModulesCommon
  {
    public static ModulePlayer   ModulePlayer { get; set; }  = new ModulePlayer();
    public static ModuleUpgrades ModuleUpgrade { get; set; } = new ModuleUpgrades();
  }
}