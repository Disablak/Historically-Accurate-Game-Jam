using System;
using System.Linq;
using Core;
using UnityEngine;
using Upgrades;

namespace UI.Hub
{
  public class HubUI : MonoBehaviour
  {
    [SerializeField] private GameObject upgradePrefab;
    [SerializeField] private Transform  containerTransform;
    [SerializeField] private bool       is_left = true;

    private const int COUNT_IN_COLON = 4;

    private void Start()
    {
      var upgrades = is_left ? ModulesCommon.ModuleUpgrade.miningUpgrades.Take( COUNT_IN_COLON ) : ModulesCommon.ModuleUpgrade.miningUpgrades.Skip( COUNT_IN_COLON );
      foreach (UpgradeBase upgrade in upgrades)
      {
        UpgradeButton upgrade_button = Instantiate(upgradePrefab, containerTransform.transform).GetComponent<UpgradeButton>();
        upgrade_button.init(upgrade);
      }
    }
  }
}