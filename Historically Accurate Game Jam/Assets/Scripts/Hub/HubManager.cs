using System;
using System.Collections;
using Core;
using UnityEngine;


namespace Hub
{
  public class HubManager : MonoBehaviour
  {
    [SerializeField] private SalesWindow salesWindow;
    [SerializeField] private ImageFade   imageFade;
    [SerializeField] private GameObject  endGO;


    private void Awake()
    {
      SaleResult sale_result = ModulesCommon.ModuleSales.sellResourced(ModulesCommon.ModulePlayer.resourcesMined);
      if (sale_result.totalSold() > 0)
        salesWindow.init(sale_result);

      ModulesCommon.onAirshipBought += theEnd;
    }

    public void goToMine()
    {
      Invoke( "loadNextScene", 0.6f );
    }

    private void loadNextScene() =>
      ModulesCommon.loadNextScene();

    private void theEnd()
    {
      StartCoroutine(fade());
    }

    private IEnumerator fade()
    {
      imageFade.enableFade(false);
      yield return new WaitForSeconds(0.6f);
      endGO.SetActive(true);
      imageFade.enableFade(true);
    }
  }
}