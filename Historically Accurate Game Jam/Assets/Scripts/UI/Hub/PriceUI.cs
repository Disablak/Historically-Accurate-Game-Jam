using TMPro;
using UnityEngine;

namespace UI.Hub
{
  public class PriceUI : MonoBehaviour
  {
    [SerializeField] private TMP_Text text;


    public void setText(int amount)
    {
      text.SetText(amount.ToString());
    }
  }
}