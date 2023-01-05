using System.Collections.Generic;
using Core;

namespace Upgrades
{
  public class PlayerUpgrades
  {
    public int miningTimeLevel           { get; set; } = 0;
    public Dictionary<ResourceType, int> miningPermanentBonusLevel { get; set; } = new Dictionary<ResourceType, int>();
    public int miningDoubleMineBonus     { get; set; } = 0;
    public int cartCapacityLevel         { get; set; } = 0;

    public PlayerUpgrades()
    {
      foreach (ResourceType resource_type in ResourceTypeHelper.allValues)
        miningPermanentBonusLevel[resource_type] = 2;
    }
  }
}