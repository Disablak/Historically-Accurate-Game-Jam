using Core;


namespace Upgrades
{
  public class MU_PermanentResource : UpgradeBase
  {
    public ResourceType[] resourceTypes  { get; set; }
    public int[]          amountByLevel { get; set; }


    public MU_PermanentResource(ResourceType[] resource_types, int[] amount_by_level, int[] diamond_price, int[] money_price)
        : base(amount_by_level.Length, diamond_price, money_price)
    {
      resourceTypes = resource_types;
      amountByLevel = amount_by_level;
    }

    public int getAmountForLevel(int level)
    {
      if (level < 1)
        return 0;

      return amountByLevel[level - 1];
    }

    public override string getDescriptionString(int level)
    {
      return $"Adds {getAmountForLevel(level)} of {resourceTypes.toString()} every time you get it";
    }

    public override ResourceType[] getAffectedResources()
    {
      return resourceTypes;
    }
  }
}