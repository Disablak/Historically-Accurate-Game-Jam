using System;
using Core;
using UnityEngine;

namespace Hub
{
  public class HubManager : MonoBehaviour
  {
    private void Awake()
    {
      ModulesCommon.ModuleSales.sellResourced(ModulesCommon.ModulePlayer.resourcesMined);
    }

    public void goToMine()
    {
      ModulesCommon.loadNextScene();
    }
  }
}