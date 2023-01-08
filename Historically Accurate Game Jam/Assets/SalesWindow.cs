using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using Hub;
using TMPro;
using UnityEngine;


public class SalesWindow : MonoBehaviour
{
  [SerializeField] private TMP_Text    windowText;
  [SerializeField] private AudioSource audioSource;


  public void init(SaleResult sale_result)
  {
    string text = string.Empty;
    foreach (ResourceType resource_type in ResourceTypeHelper.allValues.Except(new [] {ResourceType.DIAMOND}))
    {
      text += $"{resource_type.ToString()}: {sale_result.resourcesSold[resource_type]} x {sale_result.resourcesPrice[resource_type]} = {sale_result.getMoney(resource_type)}\n";
    }

    text += $"Total money: {sale_result.totalSold()}\n";
    text += $"{ResourceType.DIAMOND.ToString()} = {sale_result.totalDiamondsCount}";

    windowText.SetText(text);
    gameObject.SetActive(true);
    audioSource.Play();
  }

  public void closeWindow()
  {
    gameObject.SetActive(false);
  }
}
