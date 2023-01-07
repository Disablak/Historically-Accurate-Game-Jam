using System.Collections.Generic;
using System.Reflection;
using Core;

namespace Player
{
  public class ModulePlayer
  {
    public Core.Player player { get; private set; } = new Core.Player();

    public Dictionary<ResourceType, int> resourcesMined { get; set; } = new Dictionary<ResourceType, int>();


    public void addResource(ResourceType resource_type, int amount)
    {
      if (!resourcesMined.ContainsKey(resource_type))
        resourcesMined[resource_type] = 0;

      resourcesMined[resource_type] += amount;
    }
  }
}