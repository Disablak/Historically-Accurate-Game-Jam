using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceSold : MonoBehaviour
{
  [SerializeField] private TMP_Text text;


  public void setText(int resource_sold, int price, int total_sold)
  {
    if (resource_sold < 1)
      gameObject.SetActive(false);
    else
      text.SetText($"{resource_sold} x {price} = {total_sold}\n");

    gameObject.SetActive(true);
  }
}
