using System;
using System.Collections.Generic;

namespace Upgrades
{
  public class MU_MiningTime : UpgradeBase
  {
    public int[] secondsForLevel { get; private set; }


    public MU_MiningTime(int[] bonus_seconds_for_level)
        : base(bonus_seconds_for_level.Length)
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
      throw new System.NotImplementedException();
    }
  }
}