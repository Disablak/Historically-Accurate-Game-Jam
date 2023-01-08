namespace Upgrades
{
  public class CU_GuaranteeResource : UpgradeBase
  {
    public int[] percentByLevel;


    public CU_GuaranteeResource(int[] percent_by_level, int[] diamond_price, int[] money_price)
        : base(percent_by_level.Length, diamond_price, money_price)
    {
      percentByLevel = percent_by_level;
    }

    public int getPercentByLevel(int level)
    {
      if (level < 1)
        return 0;

      return percentByLevel[level - 1];
    }

    public override string getDescriptionString(int level)
    {
      return $"Can't lose more than {100 - getPercentByLevel(level)} on karting stage";
    }
  }
}