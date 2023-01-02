using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
  public class ClickerUI : MonoBehaviour
  {
    [SerializeField] private TMP_Text resourceText;
    [SerializeField] private TMP_Text diamondText;
    [SerializeField] private TimerUI  timerUI;
    [SerializeField] private TMP_Text endgameText;
    [SerializeField] private Object   resourcePopupPrefab;
    [SerializeField] private Object   diamondPopupPrefab;

    public void resourceMined(int count, int total)
    {
      resourceText.SetText(total.ToString());
      ResourcePopupUI resource_popup_ui = Instantiate(resourcePopupPrefab, Input.mousePosition, Quaternion.identity).GetComponent<ResourcePopupUI>();
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