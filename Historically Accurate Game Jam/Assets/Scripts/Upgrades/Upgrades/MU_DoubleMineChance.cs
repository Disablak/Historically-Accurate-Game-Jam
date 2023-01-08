using Core;

namespace Upgrades
{
  public class MU_DoubleMineChance : UpgradeBase
  {
    public ResourceType[] resourceTypes  { get; set; }
    public int[]        bonusForLevel { get; set; }
    public bool         modifyBonus   { get; set; }


    public MU_DoubleMineChance(ResourceType[] resource_types, int[] bonus_for_level, bool modify_bonus, int[] diamond_price, int[] money_price)
        : base(bonus_for_level.Length, diamond_price, money_price)
    {
      resourceTypes = resource_types;
      bonusForLevel = bonus_for_level;
      modifyBonus   = modify_bonus;
    }

    public int getDoubleChanceForLevel(int level)
    {
      if (level < 1)
        return 0;

      return bonusForLevel[level - 1];
    }

    public override string getDescriptionString(int level)
    {
      if (level == 1)
        return $"Adds a {getDoubleChanceForLevel(level)}% chance to get double {resourceTypes.toString()}";
      else
        return $"Upgrades a chance to get double {resourceTypes.toString()} by {getDoubleChanceForLevel(level)}";
    }

    public override ResourceType[] getAffectedResources()
    {
      return resourceTypes;
    }
  }
}