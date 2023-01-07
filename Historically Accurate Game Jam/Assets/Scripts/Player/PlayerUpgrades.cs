using System.Collections.Generic;
using Core;

namespace Upgrades
{
  public class PlayerUpgrades
  {
    public const int DEFAULT_LEVEL = 0;

    public Dictionary<UpgradeBase, int> upgradeLevels { get; set; } = new Dictionary<UpgradeBase, int>();
    public Dictionary<ResourceType, int> miningPermanentBonusLevel { get; set; } = new Dictionary<ResourceType, int>();
    public Dictionary<ResourceType, int> miningBonusChanceLevel    { get; set; } = new Dictionary<ResourceType, int>();
    public Dictionary<ResourceType, int> miningDoubleMineBonus     { get; set; } = new Dictionary<ResourceType, int>();
    public int miningTimeLevel   { get; set; } = DEFAULT_LEVEL;
    public int cartCapacityLevel { get; set; } = DEFAULT_LEVEL;

    public PlayerUpgrades()
    {
      foreach (ResourceType resource_type in ResourceTypeHelper.allValues)
      {
        miningPermanentBonusLevel[resource_type] = DEFAULT_LEVEL;
        miningBonusChanceLevel[resource_type]    = DEFAULT_LEVEL;
        miningDoubleMineBonus[resource_type]     = DEFAULT_LEVEL;
      }
    }

    public void upgrade(UpgradeBase upgrade_base)
    {
      if (!upgradeLevels.ContainsKey(upgrade_base))
        upgradeLevels[upgrade_base] = 1;
      else
        ++upgradeLevels[upgrade_base];
    }

    public int getBonusLevel(UpgradeBase upgrade_base)
    {
      upgradeLevels.TryGetValue(upgrade_base, out int level);
      return level;
    }
  }
}