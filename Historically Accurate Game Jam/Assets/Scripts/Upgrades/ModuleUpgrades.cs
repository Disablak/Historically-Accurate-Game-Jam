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
      registerUpgrade(new MU_MiningTime(new []{ 10, 20, 30, 40, 50 }, Array.Empty<int>(), new []{ 500, 1000, 2000, 4000, 6000} ));
      registerUpgrade(new MU_CartCapacity(new []{ 1000, 20000, 3000 }, new []{ 1, 2, 3 }, new []{ 500, 1000, 2000}));
      registerUpgrade(new MU_PermanentResource(ResourceTypeHelper.allValues.Except( new[] {ResourceType.DIAMOND} ).ToArray(), new []{ 5, 5, 5 },new []{ 500, 1000, 2000}, Array.Empty<int>() ) );
      registerUpgrade(new MU_MiningChance(new [] {ResourceType.IRON, ResourceType.GOLD}, new int[]{ 5, 5, 5 }, true, new []{ 500, 1000, 2000}, Array.Empty<int>()));
      registerUpgrade(new MU_DoubleMineChance(new [] {ResourceType.COAL, ResourceType.IRON, ResourceType.GOLD}, new []{ 2, 5, 10 }, true, Array.Empty<int>(), new []{ 1000, 2000, 3000 }));
      registerUpgrade(new MU_Helper(0.2f, Array.Empty<int>(), Array.Empty<int>()));
      registerUpgrade(new AirshipUpgrade(new []{ 10 }, new []{ 1000 }));
    }

    private void registerUpgrade(UpgradeBase upgrade)
    {
      upgrades.Add(upgrade);
    }
  }
}