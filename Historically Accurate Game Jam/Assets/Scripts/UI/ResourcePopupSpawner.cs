using System;
using Core;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI
{
  public class ResourcePopupSpawner : MonoBehaviour
  {
    [SerializeField] private Object coalPrefab;
    [SerializeField] private Object ironPrefab;
    [SerializeField] private Object goldPrefab;
    [SerializeField] private Object diamondPrefab;

    private Vector3 spawnPosition { get; set; }

    private void Update()
    {
      spawnPosition = Input.mousePosition;
    }

    public void spawnPrefab(ResourceType resource_type, int amount)
    {
      Object popup_prefab = null;
      switch (resource_type)
      {
        case ResourceType.COAL:    popup_prefab = coalPrefab; break;
        case ResourceType.IRON:    popup_prefab = ironPrefab; break;
        case ResourceType.GOLD:    popup_prefab = goldPrefab; break;
        case ResourceType.DIAMOND: popup_prefab = diamondPrefab; break;
      }
      if (popup_prefab == null)
        return;

      ResourcePopupUI resource_popup_script = Instantiate(popup_prefab, transform).GetComponent<ResourcePopupUI>();
      resource_popup_script.gameObject.transform.position = spawnPosition;
      resource_popup_script.fade(amount);
    }
  }
}