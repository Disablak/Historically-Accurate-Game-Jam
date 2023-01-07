namespace Upgrades
{
  public class KU_GuaranteeResource : UpgradeBase
  {
    public int[] percentByLevel;


    public KU_GuaranteeResource(int max_level, int[] diamond_price, int[] money_price)
        : base(max_level, diamond_price, money_price)
    {
    }

    public override string getDescriptionString(int level)
    {
      throw new System.NotImplementedException();
    }
  }
}