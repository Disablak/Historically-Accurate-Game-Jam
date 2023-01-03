using System;
using Core;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
  public class ClickerUI : MonoBehaviour
  {
    [Header("References")]
    [SerializeField] private TMP_Text coalText;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text diamondText;
    [SerializeField] private TimerUI  timerUI;
    [SerializeField] private TMP_Text endgameText;
    [SerializeField] private Object   coalPopupPrefab;
    [SerializeField] private Object   goldPopupPrefab;
    [SerializeField] private Object   diamondPopupPrefab;

    public void resourceMined(ResourceType resource_type, int count, int total)
    {
      ResourcePopupUI resource_popup_ui = null;
      switch (resource_type)
      {
        case ResourceType.COAL:
          coalText.SetText(total.ToString());
          resource_popup_ui = Instantiate(coalPopupPrefab, Input.mousePosition, Quaternion.identity).GetComponent<ResourcePopupUI>();
          break;
        case ResourceType.GOLD:
          goldText.SetText(total.ToString());
          resource_popup_ui = Instantiate(goldPopupPrefab, Input.mousePosition, Quaternion.identity).GetComponent<ResourcePopupUI>();
          break;
        case ResourceType.DIAMOND:
          diamondText.SetText(total.ToString());
          resource_popup_ui = Instantiate(diamondPopupPrefab, Input.mousePosition, Quaternion.identity).GetComponent<ResourcePopupUI>();
          break;
      }

      if (!resource_popup_ui)
        return;

      resource_popup_ui.gameObject.transform.SetParent(gameObject.transform, true);
      resource_popup_ui.fade(count);
    }

    public void diamondMined(int count, int total)
    {
      diamondText.SetText(total.ToString());
      ResourcePopupUI resource_popup_ui = Instantiate(diamondPopupPrefab, Input.mousePosition, Quaternion.identity).GetComponent<ResourcePopupUI>();
      resource_popup_ui.gameObject.transform.SetParent(gameObject.transform, true);
      resource_popup_ui.fade(count);
    }

    public void setTimer(int seconds_left)
    {
      timerUI.setTime(seconds_left);
    }

    public void timerEnded()
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