using System.Collections.Generic;
using Core;

namespace Carting
{
  public class ModuleCart
  {
    private const float RESOURCES_LOSE_PERCENT = 0.05f;

    private Dictionary<ResourceType, int> resourcesLoseCount { get; set; } = new Dictionary<ResourceType, int>();

    public Dictionary<ResourceType, int> resourcesRemained { get; private set; } = new Dictionary<ResourceType, int>();


    public void setResourcesRemained(Dictionary<ResourceType, int> resources_mined)
    {
      foreach (ResourceType resource_type in ResourceTypeHelper.allValues)
      {
        resources_mined.TryGetValue(resource_type, out int mined);
        resourcesRemained[resource_type] = mined;
        resourcesLoseCount[resource_type] = (int) ( mined * RESOURCES_LOSE_PERCENT );
      }
    }

    public void loseResources()
    {
      foreach (ResourceType resource_type in ResourceTypeHelper.allValues)
        resourcesRemained[resource_type] -= resourcesLoseCount[resource_type];
    }

    public void endCarting()
    {
      ModulesCommon.ModulePlayer.resourcesMined = resourcesRemained;
    }
  }
}