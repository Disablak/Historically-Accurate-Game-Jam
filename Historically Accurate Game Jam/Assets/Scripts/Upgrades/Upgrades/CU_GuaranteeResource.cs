namespace Upgrades
{
  public class CU_GuaranteeResource : UpgradeBase
  {
    public int[] percentByLevel;


    public CU_GuaranteeResource(int max_level, int[] diamond_price, int[] money_price)
        : base(max_level, diamond_price, money_price)
    {
    }

    public override string getDescriptionString(int level)
    {
      throw new System.NotImplementedException();
    }
  }
}