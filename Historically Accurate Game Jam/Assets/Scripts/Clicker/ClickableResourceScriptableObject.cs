using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

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


    public ResourceValueChance getResourceValueChanceByType(ResourceType resource_type)
      => resource_type switch
         {
           ResourceType.COAL    => coal,
           ResourceType.IRON    => iron,
           ResourceType.GOLD    => gold,
           ResourceType.DIAMOND => diamond,

           _ => throw new ArgumentOutOfRangeException(nameof(resource_type), resource_type, null)
         };

    public List<ResourceValueChance> all => new List<ResourceValueChance>(){coal, iron, gold, diamond};

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