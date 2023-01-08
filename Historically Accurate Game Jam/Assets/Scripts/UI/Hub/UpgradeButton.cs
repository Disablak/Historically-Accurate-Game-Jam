using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Upgrades;

namespace UI.Hub
{
  public class UpgradeButton : MonoBehaviour
  {
    [SerializeField] private TMP_Text         levelText;
    [SerializeField] private TMP_Text         descriptionText;
    [SerializeField] private Slider           progressBar;
    [SerializeField] private PriceContainerUI priceContainer;
    [SerializeField] private AudioSource      audioSource;

    private UpgradeBase upgrade { get; set; }

    private int curLevel { get; set; }
    private int maxLevel { get; set; }

    private PlayerUpgrades playerUpgrades => ModulesCommon.ModulePlayer.player.playerUpgrades;


    private void Awake()
    {
      gameObject.SetActive(false);
    }

    public void init(UpgradeBase upgrade)
    {
      this.upgrade = upgrade;

      maxLevel = upgrade.maxLevel;
      curLevel = playerUpgrades.getBonusLevel(upgrade);
      progressBar.maxValue = maxLevel;
      progressBar.minValue = PlayerUpgrades.DEFAULT_LEVEL; 
      setValues(curLevel);
      gameObject.SetActive(true);
    }

    private void setValues(int level)
    {
      priceContainer.setPrice(upgrade.getDiamondPriceForLevel(curLevel + 1), upgrade.getMoneyPriceForLevel(curLevel + 1));
      setProgressBar(level);
      setLevelText(level);
      if (level + 1 > maxLevel)
        setDescriptionMaxedText();
      else
        setDescriptionText(level + 1);
    }

    private void setProgressBar(int level)
    {
      progressBar.value = level;
    }

    private void setLevelText(int cur_level)
    {
      levelText.SetText($"Level {cur_level}/{upgrade.maxLevel}");
    }

    private void setDescriptionText(int level)
    {
      descriptionText.SetText(upgrade.getDescriptionString(level));
    }

    private void setDescriptionMaxedText()
    {
      descriptionText.SetText("Maxed, YAY");
    }

    public void onClick()
    {
      if (curLevel >= maxLevel)
        return;

      if (!ModulesCommon.ModulePlayer.player.trySpendMoney(upgrade.getMoneyPriceForLevel(curLevel + 1)))
        return;

      if (!ModulesCommon.ModulePlayer.player.trySpendDiamonds(upgrade.getDiamondPriceForLevel(curLevel + 1)))
        return;

      playerUpgrades.upgrade(upgrade);
      audioSource.Play();
      setValues(++curLevel);
    }
  }
}