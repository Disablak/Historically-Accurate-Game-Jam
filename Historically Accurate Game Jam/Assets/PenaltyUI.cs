using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;

public class PenaltyUI : MonoBehaviour
{
  private TextMeshProUGUI txt;

  public void Start()
  {
    ModulesCommon.ModuleCart.on_change_penalty += updateText;

    txt                                        =  GetComponent<TextMeshProUGUI>();
    updateText(0);
  }

  public void updateText(int penalty) =>
    txt.text = $"Penalty percentage:  {penalty}";
}
