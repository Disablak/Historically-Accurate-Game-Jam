using System;
using System.Collections.Generic;

namespace Upgrades
{
  public class MU_MiningTime : UpgradeBase
  {
    public int[] secondsForLevel { get; private set; }


    public MU_MiningTime(int[] bonus_seconds_for_level, int[] diamond_price, int[] money_price)
        : base(bonus_seconds_for_level.Length, diamond_price, money_price)
    {
      secondsForLevel = bonus_seconds_for_level;
    }

    public int getSecondsForLevel(int level)
    {
      if (level < 1)
        return 0;

      return secondsForLevel[level - 1];
    }

    public override string getDescriptionString(int level)
    {
      return $"Adds {getSecondsForLevel(level)} seconds on mining stage";
    }
  }
}