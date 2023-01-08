namespace Upgrades
{
  public class MU_Helper : UpgradeBase
  {
    public float mineSeconds { get; private set; }

    public MU_Helper(float mine_seconds, int[] diamond_price, int[] money_price)
        : base(1, diamond_price, money_price)
    {
      mineSeconds = mine_seconds;
    }

    public override string getDescriptionString(int level)
    {
      return $"Hire a assistant, that will mine resources with you every {mineSeconds} seconds";
    }
  }
}