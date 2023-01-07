using Core;

namespace Upgrades
{
  public class MU_MiningChance : UpgradeBase
  {
    public ResourceType[] resourceTypes { get; set; }

    public int[] chanceForLevel { get; set; }

    public bool modifyBonus { get; set; }


    public MU_MiningChance(ResourceType[] resource_types, int[] chance_by_level, bool modify_bonus, int[] diamond_price, int[] money_price)
        : base(chance_by_level.Length, diamond_price, money_price)
    {
      resourceTypes  = resource_types;
      chanceForLevel = chance_by_level;
      modifyBonus    = modify_bonus;
    }

    public int getBonusChanceForLevel(int level)
    {
      if (level < 1)
        return 0;

      return chanceForLevel[level - 1];
    }

    public override string getDescriptionString(int level)
    {
      return $"Upgrades a chance to get {resourceTypes.toString()} by {getBonusChanceForLevel(level)}";
    }
  }
}