using System;
using UI.Tween;
using UnityEngine;


namespace UI
{
  public class TweenScale : Tween.Tween
  {
    [Header("Settings")]
    [SerializeField] private float tweenScaleSize;
    [SerializeField] private float tweenScaleTime;

    private Vector3 _default_scale;
    private Vector3 _tween_scale;

    private void Awake()
    {
      _default_scale = transform.localScale;
      _tween_scale   = _default_scale * tweenScaleSize;
    }

    public override void tween()
    {
      LeanTween.cancel(gameObject);
      transform.localScale = _default_scale;
      LeanTween.scale(gameObject, _tween_scale, tweenScaleTime).setEasePunch();
    }
  }
}