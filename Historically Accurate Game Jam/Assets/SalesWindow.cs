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
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private ResourceSold coalSold;
  [SerializeField] private ResourceSold ironSold;
  [SerializeField] private ResourceSold goldSold;
  [SerializeField] private TMP_Text     diamondText;
  [SerializeField] private TMP_Text     moneyText;


  public void init(SaleResult sale_result)
  {
    foreach (ResourceType resource_type in ResourceTypeHelper.allValues.Except(new [] {ResourceType.DIAMOND}))
    {
      ResourceSold resource_sold = null;
      switch (resource_type)
      {
        case ResourceType.COAL: resource_sold = coalSold; break;
        case ResourceType.IRON: resource_sold = ironSold; break;
        case ResourceType.GOLD: resource_sold = goldSold; break;
      }
      if (resource_sold)
        resource_sold.setText(sale_result.resourcesSold[resource_type], sale_result.resourcesPrice[resource_type], sale_result.getMoney(resource_type));
    }

    diamondText.SetText(sale_result.totalDiamondsCount.ToString());
    moneyText.SetText(sale_result.totalSold().ToString());

    gameObject.SetActive(true);
    audioSource.Play();
  }

  public void closeWindow()
  {
    gameObject.SetActive(false);
  }
}
