using System.Collections;
using System.Collections.Generic;
using Core;
using UI.Tween;
using UnityEngine;

namespace Clicker
{
  [RequireComponent(typeof(Collider2D))]
  public class ClickableResource : MonoBehaviour, ITweenable
  {
    [Header("References")]
    [SerializeField] private Tween tweener;

    [Header("Settings")]
    [SerializeField] private bool                             deleteOnClicked;
    [SerializeField] private bool                             isBonus;
    [SerializeField] private long                             lifetime;

    public Collider2D collider2D;

    public delegate void ResourceClickedHandler(bool is_bonus);
    public event ResourceClickedHandler onClicked;


    private void Awake()
    {
      if (lifetime > 0)
        StartCoroutine(destroyCoroutine());
    }

    private void OnMouseDown()
    {
      onClicked?.Invoke(isBonus);

      if (!deleteOnClicked)
        return;

      Destroy(gameObject);
    }

    private IEnumerator destroyCoroutine()
    {
      yield return new WaitForSeconds(lifetime);
      Destroy(gameObject);
    }

    public void tween()
    {
      if (tweener)
        tweener.tween();
    }
  }
}
