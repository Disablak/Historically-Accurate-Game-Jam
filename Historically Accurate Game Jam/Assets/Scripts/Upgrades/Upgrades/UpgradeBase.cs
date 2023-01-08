using System;
using Core;

namespace Upgrades
{
  public abstract class UpgradeBase
  {
    public int maxLevel { get; protected set; }

    private int[] moneyPrice   { get; set; }
    private int[] diamondPrice { get; set; }


    public UpgradeBase(int max_level, int[] diamond_price, int[] money_price)
    {
      maxLevel = max_level;
      moneyPrice = money_price;
      diamondPrice = diamond_price;
    }

    public int getDiamondPriceForLevel(int level)
    {
      if (diamondPrice.Length < level)
        return 0;

      return diamondPrice[level - 1];
    }

    public int getMoneyPriceForLevel(int level)
    {
      if (moneyPrice.Length < level)
        return 0;

      return moneyPrice[level - 1];
    }

    public abstract string getDescriptionString(int level);

    public virtual ResourceType[] getAffectedResources() => Array.Empty<ResourceType>();
  }
}