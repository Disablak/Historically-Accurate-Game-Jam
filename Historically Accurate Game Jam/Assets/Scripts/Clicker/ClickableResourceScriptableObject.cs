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
    public ResourceValueChance coal;
    public ResourceValueChance gold;
    public ResourceValueChance diamond;

    public ResourceValueChance getResourceValueChanceByType(ResourceType resource_type)
      => resource_type switch
         {
           ResourceType.COAL    => coal,
           ResourceType.GOLD    => gold,
           ResourceType.DIAMOND => diamond,

           _ => throw new ArgumentOutOfRangeException(nameof(resource_type), resource_type, null)
         };

    public List<ResourceValueChance> all => new List<ResourceValueChance>(){coal, gold, diamond};

    public Tuple<ResourceType, int> getRandomResourceTypeWithValue() // Piece of shit, but I am to lazy to do anything better
    {
      int random_value = Random.Range(0, 100);
      if (coal.randomRange.minInclusive <= random_value && coal.randomRange.maxExclusive > random_value)
        return new Tuple<ResourceType, int>(ResourceType.COAL, coal.amount);
      else
      if (gold.randomRange.minInclusive <= random_value && gold.randomRange.maxExclusive > random_value)
        return new Tuple<ResourceType, int>(ResourceType.GOLD, gold.amount);
      else
      if (diamond.randomRange.minInclusive <= random_value && diamond.randomRange.maxExclusive > random_value)
        return new Tuple<ResourceType, int>(ResourceType.DIAMOND, diamond.amount);

      return new Tuple<ResourceType, int>(ResourceType.NONE, random_value);
    }
  }
}