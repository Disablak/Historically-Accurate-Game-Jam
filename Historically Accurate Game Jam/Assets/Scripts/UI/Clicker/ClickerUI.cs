using Core;
using TMPro;
using UnityEngine;

namespace UI.Clicker
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

    public void resourceMined(ResourceType resource_type, int amount, int total, bool is_helper)
    {
      resourcesBalance.setResourceBalance(resource_type, total);
      if (!is_helper)
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

    public void gameEnded(bool is_ended_time)
    {
      endgameText.gameObject.SetActive(true);
      endgameText.text = is_ended_time ? "Time is over" : "Cart is full";
      timerUI.finishPulsation();
    }

    public void timerEnding()
    {
      timerUI.startPulsation();
    }
  }
}