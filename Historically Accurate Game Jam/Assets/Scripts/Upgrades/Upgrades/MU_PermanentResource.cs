using Core;


namespace Upgrades
{
  public class MU_PermanentResource : UpgradeBase
  {
    public ResourceType resourceType  { get; set; }
    public int[]        amountByLevel { get; set; }


    public MU_PermanentResource(ResourceType resource_type, int[] amount_by_level)
        : base(amount_by_level.Length)
    {
      resourceType = resource_type;
      amountByLevel =amount_by_level;
    }

    public int getAmountForLevel(int level)
    {
      if (level < 1)
        return 0;

      return amountByLevel[level - 1];
    }

    public override string getDescriptionString()
    {
      throw new System.NotImplementedException();
    }
  }
}