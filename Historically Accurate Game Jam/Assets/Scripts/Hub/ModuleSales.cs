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
      SaleResult sale_result = new SaleResult(coalPrice, ironPrice, goldPrice);
      foreach (ResourceType resource_type in ResourceTypeHelper.allValues.Except(new[] {ResourceType.DIAMOND}))
      {
        resources_brought.TryGetValue(resource_type, out int mined);
        sale_result.resourcesSold[resource_type] = mined;
      }

      if (resources_brought.ContainsKey(ResourceType.DIAMOND))
        sale_result.totalDiamondsCount = resources_brought[ResourceType.DIAMOND];

      ModulesCommon.ModulePlayer.player.moneyBalance    += sale_result.totalMoneyCount;
      ModulesCommon.ModulePlayer.player.diamondsBalance += sale_result.totalDiamondsCount;

      return sale_result;
    }
  }

  public class SaleResult
  {
    public int                           totalDiamondsCount;
    public Dictionary<ResourceType, int> resourcesSold = new Dictionary<ResourceType, int>();
    public int                           coalPrice;
    public int                           ironPrice;
    public int                           goldPrice;

    public SaleResult(int coal_price, int iron_price, int gold_price)
    {
      coalPrice = coal_price;
      ironPrice = iron_price;
      goldPrice = gold_price;
    }

    public int coalMoney => resourcesSold[ResourceType.COAL] * coalPrice;
    public int ironMoney => resourcesSold[ResourceType.IRON] * ironPrice;
    public int goldMoney => resourcesSold[ResourceType.GOLD] * goldPrice;

    public int totalMoneyCount => coalMoney + ironMoney + goldMoney;
  }
}