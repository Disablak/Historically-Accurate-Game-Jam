using System;
using Carting;
using Hub;
using Player;
using Upgrades;

namespace Core
{
  public static class ModulesCommon
  {
    public static ModulePlayer   ModulePlayer  { get; set; }  = new ModulePlayer();
    public static ModuleUpgrades ModuleUpgrade { get; set; } = new ModuleUpgrades();
    public static ModuleSales    ModuleSales   { get; set; } = new ModuleSales();
    public static ModuleCart     ModuleCart    { get; set; } = new ModuleCart();
    public static SceneLoader    SceneLoader   { get; set; }
    public static MusicManager   musicManager  { get; set; }

    public static event Action onAirshipBought; 

    public static void loadNextScene()
    {
      int cur_scene = SceneLoader.loadNextScene();
      musicManager.playSceneMusic(cur_scene);
    }

    public static void airshipBought()
    {
      onAirshipBought?.Invoke();
    }
  }
}