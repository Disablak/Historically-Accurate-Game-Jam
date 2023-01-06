using Core;

namespace Upgrades
{
  public class MU_MiningChance : UpgradeBase
  {
    public ResourceType resourceType { get; set; }

    public int[] chanceForLevel { get; set; }

    public bool modifyBonus { get; set; }


    public MU_MiningChance(ResourceType resource_type, int[] chance_by_level, bool modify_bonus)
        : base(chance_by_level.Length)
    {
      resourceType   = resource_type;
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
      throw new System.NotImplementedException();
    }
  }
}