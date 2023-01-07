using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.Hub;
using UnityEngine;

public class PriceContainerUI : MonoBehaviour
{
  [SerializeField] private PriceUI diamond;
  [SerializeField] private PriceUI money;


  public void setPrice(int diamond_price, int money_price)
  {
    if (diamond_price > 0)
      diamond.setText(diamond_price);
    else
      diamond.gameObject.SetActive(false);

    if (money_price > 0)
      money.setText(money_price);
    else
      money.gameObject.SetActive(false);
  }
}
