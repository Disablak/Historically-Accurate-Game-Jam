using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartCapacityUI : MonoBehaviour
{
  [SerializeField] private Slider   progressBar;
  [SerializeField] private TMP_Text capacityText;

  private int maxValue { get; set; }

  public void setMaxCapacity(int max_capacity)
  {
    maxValue = max_capacity;
    progressBar.maxValue = max_capacity;
    setCapacityText(0, max_capacity);
  }

  public void setCapacity(int amount)
  {
    progressBar.value = amount;
    setCapacityText(amount, maxValue);
  }

  private void setCapacityText(int cur_value, int max_value)
  {
    capacityText.SetText($"{cur_value}/{max_value}");
  }
}
