using System.Collections.Generic;
using System.Reflection;
using Core;

namespace Player
{
  public class ModulePlayer
  {
    public Dictionary<ResourceType, int> balance { get; private set; } = new Dictionary<ResourceType, int>();


    public void addResource(ResourceType resource_type, int amount)
    {
      if (!balance.ContainsKey(resource_type))
        balance[resource_type] = 0;

      balance[resource_type] += amount;
    }
  }
}