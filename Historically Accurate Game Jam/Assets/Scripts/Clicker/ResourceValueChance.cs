using System;
using UnityEngine;

namespace Clicker
{
  [Serializable]
  public class ResourceValueChance
  {
    [SerializeField] public int         amount;
    [SerializeField] public RandomRange randomRange;
  }

  [Serializable]
  public struct RandomRange
  {
    public int minInclusive;
    public int maxExclusive;
  }
}