using System.Collections.Generic;
using System.Linq;
using Core;
using Player;

namespace Hub
{
  public class ModuleSales
  {
    private int coalPrice { set; get; } = 1;
    private int ironPrice { get; set; } = 5;
    private int goldPrice { get; set; } = 20;


    public SaleResult sellResourced(Dictionary<ResourceType, int> resources_brought)
    {
      SaleResult sale_result = new SaleResult()
      {
          resourcesPrice = new Dictionary<ResourceType, int>()
          {
              { ResourceType.COAL, coalPrice },
              { ResourceType.IRON, ironPrice},
              { ResourceType.GOLD, goldPrice }
          }
      };
      foreach (ResourceType resource_type in ResourceTypeHelper.allValues.Except(new[] {ResourceType.DIAMOND}))
      {
        resources_brought.TryGetValue(resource_type, out int mined);
        sale_result.resourcesSold[resource_type] = mined;
      }

      if (resources_brought.ContainsKey(ResourceType.DIAMOND))
        sale_result.totalDiamondsCount = resources_brought[ResourceType.DIAMOND];

      ModulesCommon.ModulePlayer.player.moneyBalance    += sale_result.totalSold();
      ModulesCommon.ModulePlayer.player.diamondsBalance += sale_result.totalDiamondsCount;

      return sale_result;
    }
  }

  public class SaleResult
  {
    public int                           totalDiamondsCount;
    public Dictionary<ResourceType, int> resourcesSold  = new Dictionary<ResourceType, int>();
    public Dictionary<ResourceType, int> resourcesPrice = new Dictionary<ResourceType, int>();

    public int getMoney(ResourceType resource_type)
    {
      return resourcesSold[resource_type] * resourcesPrice[resource_type];
    }

    public int totalSold()
    {
      int res = 0;
      foreach (ResourceType resource_type in resourcesSold.Keys)
        res += getMoney(resource_type);

      return res;
    }
  }
}