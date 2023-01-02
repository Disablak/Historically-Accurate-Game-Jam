using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
  [RequireComponent(typeof(RectTransform), typeof(TMP_Text))]
  public class ResourcePopupUI : MonoBehaviour
  {
    [SerializeField] private TMP_Text      text;
    [SerializeField] private int           animationTime;

    private void Awake()
    {
      gameObject.SetActive(false);
    }

    public void fade(int value)
    {
      text.SetText($"+{value.ToString()}");
      gameObject.SetActive(true);
      LeanTween.value(gameObject, setTextAlpha, text.alpha, 0.0f, animationTime).setDestroyOnComplete( true );
      LeanTween.moveLocalY(gameObject, gameObject.transform.localPosition.y + 300f, animationTime);
    }

    private void setTextAlpha(float value)
    {
      text.alpha = value;
    }
  }
}