using System.Collections.Generic;
using System.Linq;
using Core;

namespace Upgrades
{
  public class PlayerUpgrades
  {
    public const int DEFAULT_LEVEL = 0;

    private Dictionary<UpgradeBase, int> upgradeLevels { get; set; } = new Dictionary<UpgradeBase, int>();

    public bool hasHelper => upgradeLevels.Keys.OfType<MU_Helper>().Any();

    public void upgrade(UpgradeBase upgrade_base)
    {
      if (!upgradeLevels.ContainsKey(upgrade_base))
        upgradeLevels[upgrade_base] = 1;
      else
        ++upgradeLevels[upgrade_base];

      if (upgrade_base is AirshipUpgrade)
        ModulesCommon.airshipBought();

      if (upgrade_base is CU_GuaranteeResource guarantee_resource)
        ModulesCommon.ModuleCart.guaranteeResources = guarantee_resource.getPercentByLevel(getBonusLevel(upgrade_base));
    }

    public int getBonusLevel(UpgradeBase upgrade_base)
    {
      upgradeLevels.TryGetValue(upgrade_base, out int level);
      return level;
    }
  }
}