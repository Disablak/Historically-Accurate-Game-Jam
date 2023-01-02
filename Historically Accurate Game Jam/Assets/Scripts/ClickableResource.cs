using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class ClickableResource : MonoBehaviour
{
   [SerializeField] private int resourceCount;
   [SerializeField] private bool deleteOnClicked;
   [SerializeField] private long lifetime;

   public Collider2D collider2D;

   public delegate void ResourceClickedHandler(int resource);
   public event ResourceClickedHandler onClicked;


   public void tween()
   {
     LeanTween.cancel(gameObject);
     transform.localScale = Vector3.one;
     LeanTween.scale(gameObject, Vector3.one * 4.0f, 0.3f).setEasePunch();
     //LeanTween.scale(gameObject, Vector3.one * 4.0f, 0.3f ).setLoopPingPong();
   }

   private void Awake()
   {
     if (lifetime > 0)
      StartCoroutine(destroyCoroutine());
   }

   private void OnMouseDown()
   {
     onClicked?.Invoke(resourceCount);
     if (deleteOnClicked)
     {
       Debug.Log("Bonus clicked");
       Destroy(gameObject);
     }
   }

   private IEnumerator destroyCoroutine()
   {
     yield return new WaitForSeconds(lifetime);
     Destroy(gameObject);
   }
}
