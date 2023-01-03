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
    [SerializeField] public ClickableResourceScriptableObject resourceSettings;
    [SerializeField] private bool                             deleteOnClicked;
    [SerializeField] private long                             lifetime;

    private Dictionary<ResourceType, ResourceValueChance> resourceValueChanceSettings;

    public Collider2D collider2D;

    public delegate void ResourceClickedHandler(ClickableResourceScriptableObject settings);
    public event ResourceClickedHandler onClicked;


    private void Awake()
    {
      foreach (ResourceType resource_type in ResourceTypeHelper.allValues)
        resourceValueChanceSettings[resource_type] = resourceSettings.getResourceValueChanceByType(resource_type);

      if (lifetime > 0)
        StartCoroutine(destroyCoroutine());
    }

    private void OnMouseDown()
    {
      onClicked?.Invoke(resourceSettings);

      if (!deleteOnClicked)
        return;

      Debug.Log("Bonus clicked");
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
