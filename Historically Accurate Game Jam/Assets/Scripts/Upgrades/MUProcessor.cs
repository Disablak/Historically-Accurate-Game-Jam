﻿using Core;

namespace Upgrades
{
  public static class MUProcessor
  {
    public static PlayerUpgrades player => ModulesCommon.ModulePlayer.player.playerUpgrades;

    public static void process(ClickerManager clicker_manager)
    {
      foreach (UpgradeBase upgrade in ModulesCommon.ModuleUpgrade.miningUpgrades)
      {
        switch (upgrade)
        {
          case MU_MiningTime mining_time:
            clicker_manager.addSecondsToMine(mining_time.getSecondsForLevel(player.miningTimeLevel));
            break;
          case MU_CartCapacity cart_capacity:
            clicker_manager.addCartCapacity(cart_capacity.getCapacityForLevel(player.cartCapacityLevel));
            break;
          case MU_PermanentResource permanent_resource:
            clicker_manager.addPermanentResourceBonus(permanent_resource.resourceType, permanent_resource.getAmountForLevel(player.miningPermanentBonusLevel[permanent_resource.resourceType]));
            break;
        }
      }
    }
  }
}