using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class AffectedResources : MonoBehaviour
{
  [SerializeField] private Object coalIcon;
  [SerializeField] private Object ironIcon;
  [SerializeField] private Object goldIcon;
  [SerializeField] private Object diamondIcon;

  public void spawnResources(ResourceType[] resource_types)
  {
    foreach (ResourceType resource_type in resource_types)
    {
      Object icon_prefab = null;
      switch (resource_type)
      {
        case ResourceType.COAL    : icon_prefab = coalIcon; break;
        case ResourceType.IRON    : icon_prefab = ironIcon; break;
        case ResourceType.GOLD    : icon_prefab = goldIcon; break;
        case ResourceType.DIAMOND : icon_prefab = diamondIcon; break;
      }
      Instantiate(icon_prefab, transform);
    }
  }
}
