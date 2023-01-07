using System;
using System.Collections.Generic;
using Core;
using UnityEngine;


namespace Clicker
{
  [CreateAssetMenu(fileName = "ClickableResource", menuName = "ScriptableObjects/ClickableResource", order = 1)]
  public class ClickableResourceScriptableObject : ScriptableObject
  {
    private Dictionary<ResourceType, ResourceValueChance> resourceValueChanceWithType = null;

    public ResourceValueChance coal;
    public ResourceValueChance iron;
    public ResourceValueChance gold;
    public ResourceValueChance diamond;


    public Dictionary<ResourceType, ResourceValueChance> getResourceValueChanceWithType()
    {
      ensureDictionary();
      return resourceValueChanceWithType;
    }

    public void addResourceMiningAmount(ResourceType resource_type, int amount)
    {
      ensureDictionary();
      resourceValueChanceWithType[resource_type].amount += amount;
    }

    public void modifyResourceMiningChance(ResourceType resource_type, int amount)
    {
      if (resource_type == ResourceType.COAL)
        throw new NotSupportedException($"No bonus chance for {nameof(resource_type)} {resource_type}");

      for (int i = (int) ResourceType.COAL; i <= (int) resource_type; i++)
      {
        ResourceType type = (ResourceType) i;
        if (type != ResourceType.COAL)
          resourceValueChanceWithType[type].randomRange.minInclusive -= amount;

        if (type != resource_type)
          resourceValueChanceWithType[type].randomRange.maxExclusive -= amount;
        Debug.Log($"Mining chance for {nameof(type)} {type} is {resourceValueChanceWithType[type].randomRange.minInclusive}-{resourceValueChanceWithType[type].randomRange.maxExclusive}");
      }
    }

    private void ensureDictionary()
    {
      resourceValueChanceWithType ??= new Dictionary<ResourceType, ResourceValueChance>()
      {
          { ResourceType.COAL,    new ResourceValueChance(coal)    },
          { ResourceType.IRON,    new ResourceValueChance(iron)    },
          { ResourceType.GOLD,    new ResourceValueChance(gold)    },
          { ResourceType.DIAMOND, new ResourceValueChance(diamond)}
      };
    }
  }
}