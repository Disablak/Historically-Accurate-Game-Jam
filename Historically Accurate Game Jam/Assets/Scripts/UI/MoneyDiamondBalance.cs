using System;
using Core;
using TMPro;
using UnityEngine;

namespace UI
{
  public class MoneyDiamondBalance : MonoBehaviour
  {
    [SerializeField] private TMP_Text diamondsText;
    [SerializeField] private TMP_Text moneyText;


    private void Awake()
    {
      setDiamondsText(ModulesCommon.ModulePlayer.player.diamondsBalance);
      setMoneyText(ModulesCommon.ModulePlayer.player.moneyBalance);

      ModulesCommon.ModulePlayer.player.diamondBalanceUpdated += setDiamondsText;
      ModulesCommon.ModulePlayer.player.moneyBalanceUpdated   += setMoneyText;
    }

    public void setDiamondsText(int amount)
    {
      diamondsText.SetText(amount.ToString());
    }

    public void setMoneyText(int amount)
    {
      moneyText.SetText(amount.ToString());
    }
  }
}