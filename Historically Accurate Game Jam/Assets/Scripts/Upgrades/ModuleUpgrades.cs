using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace Upgrades
{
  public class ModuleUpgrades
  {
    public List<UpgradeBase> upgrades { get; private set; } = new List<UpgradeBase>();

    public ModuleUpgrades()
    {
      cacheUpgrades();
    }

    private void cacheUpgrades()
    {
      registerUpgrade(new MU_MiningTime(new []{ 3, 6, 10 }, Array.Empty<int>(), new []{ 30000, 70000, 120000 } ));
      registerUpgrade(new MU_PermanentResource(new [] {ResourceType.COAL, ResourceType.IRON, ResourceType.GOLD}, new []{ 2, 4, 6 }, Array.Empty<int>(), new []{ 10000, 50000, 100000 } ) );
      registerUpgrade(new MU_DoubleMineChance(new [] {ResourceType.COAL, ResourceType.IRON, ResourceType.GOLD}, new []{ 10, 20, 30 }, true, Array.Empty<int>(), new []{ 35000, 100000, 150000 }));
      registerUpgrade(new MU_CartCapacity(new []{ 2500, 7500 }, Array.Empty<int>(), new []{ 15000, 50000 }));
      registerUpgrade(new MU_MiningChance(new [] {ResourceType.DIAMOND}, new int[]{ 1, 2 }, true, new []{ 10, 30 }, Array.Empty<int>()));
      registerUpgrade(new MU_Helper(0.2f, new []{ 10 }, Array.Empty<int>()));
      registerUpgrade(new AirshipUpgrade(new []{ 100 }, new []{ 500000 }));
    }

    private void registerUpgrade(UpgradeBase upgrade)
    {
      upgrades.Add(upgrade);
    }
  }
}