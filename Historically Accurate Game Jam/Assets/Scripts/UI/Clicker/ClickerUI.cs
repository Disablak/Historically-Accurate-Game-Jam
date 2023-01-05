using System;
using Core;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI
{
  public class ClickerUI : MonoBehaviour
  {
    [Header("References")]
    [SerializeField] private ResourcesBalance     resourcesBalance;
    [SerializeField] private ResourcePopupSpawner resourcePopupSpawner;
    [SerializeField] private CartCapacityUI       cartCapacityUI;
    [SerializeField] private TimerUI              timerUI;
    [SerializeField] private TMP_Text             endgameText;

    public void setupUI(int max_cart_capacity)
    {
      cartCapacityUI.setMaxCapacity(max_cart_capacity);
    }

    public void resourceMined(ResourceType resource_type, int amount, int total)
    {
      resourcesBalance.setResourceBalance(resource_type, total);
      resourcePopupSpawner.spawnPrefab(resource_type, amount);
    }

    public void setCartCapacity(int cur_amount)
    {
      cartCapacityUI.setCapacity(cur_amount);
    }

    public void setTimer(int seconds_left)
    {
      timerUI.setTime(seconds_left);
    }

    public void gameEnded()
    {
      endgameText.gameObject.SetActive(true);
      timerUI.finishPulsation();
    }

    public void timerEnding()
    {
      timerUI.startPulsation();
    }
  }
}