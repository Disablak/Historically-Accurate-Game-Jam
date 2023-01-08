using Core;

namespace Upgrades
{
  public static class MUProcessor
  {
    public static PlayerUpgrades player => ModulesCommon.ModulePlayer.player.playerUpgrades;

    public static void process(ClickerManager clicker_manager)
    {
      foreach (UpgradeBase upgrade in ModulesCommon.ModuleUpgrade.miningUpgrades)
      {
        int level = player.getBonusLevel(upgrade);
        switch (upgrade)
        {
          case MU_MiningTime mining_time:
            clicker_manager.addSecondsToMine(mining_time.getSecondsForLevel(level));
            break;
          case MU_CartCapacity cart_capacity:
            clicker_manager.addCartCapacity(cart_capacity.getCapacityForLevel(level));
            break;
          case MU_PermanentResource permanent_resource:
            clicker_manager.addPermanentResourceBonus(permanent_resource.resourceTypes, level);
            break;
          case MU_MiningChance mining_chance:
            clicker_manager.addBonusChance(mining_chance.resourceTypes, level, mining_chance.modifyBonus);
            break;
          case MU_DoubleMineChance double_mine_chance:
            clicker_manager.addDoubleChanceBonus(double_mine_chance.resourceTypes, level, double_mine_chance.modifyBonus);
            break;
          case MU_Helper helper:
            if (player.hasHelper)
              clicker_manager.setHelperMineSeconds(helper.mineSeconds);
            break;
        }
      }
    }
  }
}