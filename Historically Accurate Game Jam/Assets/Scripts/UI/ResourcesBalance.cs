using Core;
using TMPro;
using UnityEngine;

namespace UI
{
  public class ResourcesBalance : MonoBehaviour
  {
    public TMP_Text coal;
    public TMP_Text iron;
    public TMP_Text gold;
    public TMP_Text diamonds;


    public void setResourceBalance(ResourceType resource_type, int amount)
    {
      switch (resource_type)
      {
        case ResourceType.COAL:    coal.SetText(amount.ToString()); break;
        case ResourceType.IRON:    iron.SetText(amount.ToString()); break;
        case ResourceType.GOLD:    gold.SetText(amount.ToString()); break;
        case ResourceType.DIAMOND: diamonds.SetText(amount.ToString()); break;
      }
    }
  }
}
