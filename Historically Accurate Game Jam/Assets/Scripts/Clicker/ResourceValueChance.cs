using System;
using Core;
using UnityEngine;

namespace Clicker
{
  [Serializable]
  public class ResourceValueChance
  {
    [SerializeField] public int         amount;
    [SerializeField] public RandomRange randomRange;

    public ResourceValueChance(){}

    public ResourceValueChance(ResourceValueChance other)
    {
      amount = other.amount;
      randomRange = other.randomRange;
    }
  }

  [Serializable]
  public struct RandomRange
  {
    public int minInclusive;
    public int maxExclusive;
  }
}