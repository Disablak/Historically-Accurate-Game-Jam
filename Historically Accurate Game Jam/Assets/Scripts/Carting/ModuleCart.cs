using System;
using System.Collections.Generic;
using Core;

namespace Carting
{
  public class ModuleCart
  {
    private const int RESOURCES_LOSE_PERCENT = 5;

    private Dictionary<ResourceType, int> startResourcesCount { get; set; } = new Dictionary<ResourceType, int>();

    private int resourcesRemainedPercent { get; set; }
    public Action<int> on_change_penalty = delegate( int i ) {  };

    public int guaranteeResources { get; set; } = 0;

    public Dictionary<ResourceType, int> resourcesRemained { get; private set; } = new Dictionary<ResourceType, int>();


    public void setResourcesRemained(Dictionary<ResourceType, int> resources_mined)
    {
      foreach (ResourceType resource_type in ResourceTypeHelper.allValues)
      {
        resources_mined.TryGetValue(resource_type, out int mined);
        resourcesRemained[resource_type] = mined;
        startResourcesCount[resource_type] = mined;
      }
      resourcesRemainedPercent = 100;
    }

    public bool tryLoseResources( out Dictionary<ResourceType, int> amount_lost)
    {
      int lose_percent = resourcesRemainedPercent - guaranteeResources;
      if (lose_percent <= 1)
      {
        amount_lost = null;
        return false;
      }
      else
      if (lose_percent > RESOURCES_LOSE_PERCENT)
        lose_percent = RESOURCES_LOSE_PERCENT;

      amount_lost = new Dictionary<ResourceType, int>();

      foreach (ResourceType resource_type in ResourceTypeHelper.allValues)
      {
        int lost = (int) ((float) startResourcesCount[resource_type] * ((float) lose_percent / 100.0f));
        resourcesRemained[resource_type] -= lost;
        amount_lost[resource_type] = lost;
      }

      resourcesRemainedPercent -= lose_percent;
      on_change_penalty?.Invoke( 100 - resourcesRemainedPercent );
      return true;
    }

    public void endCarting()
    {
      ModulesCommon.ModulePlayer.resourcesMined = resourcesRemained;
    }
  }
}