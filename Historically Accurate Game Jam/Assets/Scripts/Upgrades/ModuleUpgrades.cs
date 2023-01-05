using System.Collections.Generic;
using System.Linq;
using Core;

namespace Upgrades
{
  public class ModuleUpgrades
  {
    public List<UpgradeBase> miningUpgrades { get; private set; } = new List<UpgradeBase>();
    public List<UpgradeBase> kartingUpgrade { get; private set; } = new List<UpgradeBase>();


    public ModuleUpgrades()
    {
      cacheUpgrades();
    }

    private void cacheUpgrades()
    {
      registerMiningUpgrade(new MU_MiningTime(new []{ 10, 20, 30, 40, 50 }));
      registerMiningUpgrade(new MU_CartCapacity(new []{ 1000, 20000, 3000 }));
      foreach (ResourceType resource_type in ResourceTypeHelper.allValues.Except( new[] {ResourceType.DIAMOND} ))
      {
        registerMiningUpgrade(new MU_PermanentResource(resource_type, new []{ 100, 200, 300 }));
      }
    }

    private void registerMiningUpgrade(UpgradeBase upgrade)
    {
      miningUpgrades.Add(upgrade);
    }
  }
}