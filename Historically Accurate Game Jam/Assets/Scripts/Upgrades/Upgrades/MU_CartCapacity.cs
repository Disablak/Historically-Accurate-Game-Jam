namespace Upgrades
{
  public class MU_CartCapacity : UpgradeBase
  {
    public int[] capacityForLevel { get; set; }


    public MU_CartCapacity(int[] capacity_by_level)
        : base(capacity_by_level.Length)
    {
      capacityForLevel = capacity_by_level;
    }

    public int getCapacityForLevel(int level)
    {
      if (level < 1)
        return 0;

      return capacityForLevel[level - 1];
    }

    public override string getDescriptionString()
    {
      throw new System.NotImplementedException();
    }
  }
}