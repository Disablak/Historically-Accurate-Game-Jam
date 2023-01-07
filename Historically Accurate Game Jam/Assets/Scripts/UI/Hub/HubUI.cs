using System;
using Core;
using UnityEngine;
using Upgrades;

namespace UI.Hub
{
  public class HubUI : MonoBehaviour
  {
    [SerializeField] private GameObject upgradePrefab;
    [SerializeField] private Transform  containerTransform;

    private void Start()
    {
      foreach (UpgradeBase upgrade in ModulesCommon.ModuleUpgrade.miningUpgrades)
      {
        UpgradeButton upgrade_button = Instantiate(upgradePrefab, containerTransform.transform).GetComponent<UpgradeButton>();
        upgrade_button.init(upgrade);
      }
    }
  }
}