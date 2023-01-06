namespace Upgrades
{
  public abstract class UpgradeBase
  {
    public int maxLevel { get; protected set; }


    public UpgradeBase(int max_level)
    {
      maxLevel = max_level;
    }

    public abstract string getDescriptionString(int level);
  }
}