using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
  [RequireComponent(typeof(RectTransform), typeof(TMP_Text))]
  public class TimerUI : MonoBehaviour
  {
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TMP_Text      text;
    [SerializeField] private float         onePulsationTime;

    public void setTime(int seconds_left)
    {
      text.SetText($"{seconds_left / 60:00}:{seconds_left % 60:00}");
    }

    public void startPulsation()
    {
      LeanTween.scale(gameObject, Vector3.one * 1.2f, onePulsationTime).setEasePunch().setLoopPingPong();
      text.color = Color.red;
    }

    public void finishPulsation()
    {
      LeanTween.cancel(gameObject);

      rectTransform.localScale = Vector3.one;
      text.color = Color.black;
    }
  }
}