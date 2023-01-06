using Core;

namespace Upgrades
{
  public class MU_DoubleMineChance : UpgradeBase
  {
    public ResourceType resourceType  { get; set; }
    public int[]        bonusForLevel { get; set; }
    public bool         modifyBonus   { get; set; }


    public MU_DoubleMineChance(ResourceType resource_type, int[] bonus_for_level, bool modify_bonus)
        : base(bonus_for_level.Length)
    {
      resourceType  = resource_type;
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
      throw new System.NotImplementedException();
    }
  }
}