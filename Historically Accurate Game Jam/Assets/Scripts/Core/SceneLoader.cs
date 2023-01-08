using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
  public class SceneLoader : MonoBehaviour
  {
    public const int HUB_SCENE_ID     = 0;
    public const int CLICKER_SCENE_ID = 1;
    public const int CART_SCENE_ID    = 2;

    public int curScene { get; private set; } = 0;

    private void Awake()
    {
      if (ModulesCommon.SceneLoader == null)
        ModulesCommon.SceneLoader = this;
      else
        Destroy(gameObject);

      DontDestroyOnLoad(this);
      curScene = SceneManager.GetActiveScene().buildIndex;
    }

    public int loadNextScene()
    {
      if (curScene == CART_SCENE_ID)
        curScene = HUB_SCENE_ID;
      else
        ++curScene;

      SceneManager.LoadScene(curScene);
      return curScene;
    }
  }
}