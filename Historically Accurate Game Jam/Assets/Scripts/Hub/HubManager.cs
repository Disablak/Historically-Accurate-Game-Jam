using System;
using Core;
using UnityEngine;


namespace Hub
{
  public class HubManager : MonoBehaviour
  {
    [SerializeField] private SalesWindow salesWindow;


    private void Awake()
    {
      SaleResult sale_result = ModulesCommon.ModuleSales.sellResourced(ModulesCommon.ModulePlayer.resourcesMined);
      if (sale_result.totalSold() > 0)
        salesWindow.init(sale_result);
    }

    public void goToMine()
    {
      Invoke( "loadNextScene", 0.6f );
    }

    private void loadNextScene() =>
      ModulesCommon.loadNextScene();
  }
}