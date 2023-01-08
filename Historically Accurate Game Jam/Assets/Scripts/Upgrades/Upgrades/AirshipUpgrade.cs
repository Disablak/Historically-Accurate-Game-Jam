namespace Upgrades
{
  public class AirshipUpgrade : UpgradeBase
  {
    public AirshipUpgrade(int[] diamond_price, int[] money_price)
        : base(1, diamond_price, money_price)
    {
    }

    public override string getDescriptionString(int level)
    {
      return $"Buy your dream";
    }
  }
}