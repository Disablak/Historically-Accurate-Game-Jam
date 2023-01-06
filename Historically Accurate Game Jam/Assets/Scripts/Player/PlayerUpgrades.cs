using System.Collections.Generic;
using Core;

namespace Upgrades
{
  public class PlayerUpgrades
  {
    private const int DEFAULT_LEVEL = 0;

    public int miningTimeLevel           { get; set; } = DEFAULT_LEVEL;
    public Dictionary<ResourceType, int> miningPermanentBonusLevel { get; set; } = new Dictionary<ResourceType, int>();
    public Dictionary<ResourceType, int> miningBonusChanceLevel    { get; set; } = new Dictionary<ResourceType, int>();
    public Dictionary<ResourceType, int> miningDoubleMineBonus     { get; set; } = new Dictionary<ResourceType, int>();
    public int cartCapacityLevel         { get; set; } = DEFAULT_LEVEL;

    public PlayerUpgrades()
    {
      foreach (ResourceType resource_type in ResourceTypeHelper.allValues)
      {
        miningPermanentBonusLevel[resource_type] = DEFAULT_LEVEL;
        miningBonusChanceLevel[resource_type]    = DEFAULT_LEVEL;
        miningDoubleMineBonus[resource_type]     = DEFAULT_LEVEL;
      }
    }
  }
}