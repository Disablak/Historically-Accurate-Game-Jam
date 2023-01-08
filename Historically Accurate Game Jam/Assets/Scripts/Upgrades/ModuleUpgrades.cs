using System;
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
      registerMiningUpgrade(new MU_MiningTime(new []{ 10, 20, 30, 40, 50 }, Array.Empty<int>(), new []{ 500, 1000, 2000, 4000, 6000} ));
      registerMiningUpgrade(new MU_CartCapacity(new []{ 1000, 20000, 3000 }, new []{ 1, 2, 3 }, new []{ 500, 1000, 2000}));
      registerMiningUpgrade(new MU_PermanentResource(ResourceTypeHelper.allValues.Except( new[] {ResourceType.DIAMOND} ).ToArray(), new []{ 5, 5, 5 },new []{ 500, 1000, 2000}, Array.Empty<int>() ) );
      registerMiningUpgrade(new MU_MiningChance(new [] {ResourceType.IRON, ResourceType.GOLD}, new int[]{ 5, 5, 5 }, true, new []{ 500, 1000, 2000}, Array.Empty<int>()));
      registerMiningUpgrade(new MU_DoubleMineChance(new [] {ResourceType.COAL, ResourceType.IRON, ResourceType.GOLD}, new []{ 2, 5, 10 }, true, Array.Empty<int>(), new []{ 1000, 2000, 3000 }));
      registerMiningUpgrade(new MU_Helper(0.2f, Array.Empty<int>(), Array.Empty<int>()));
    }

    private void registerMiningUpgrade(UpgradeBase upgrade)
    {
      miningUpgrades.Add(upgrade);
    }
  }
}