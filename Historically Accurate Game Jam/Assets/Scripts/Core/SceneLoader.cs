using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
  public class SceneLoader : MonoBehaviour
  {
    private const int HUB_SCENE_ID     = 0;
    private const int CLICKER_SCENE_ID = 1;
    private const int CART_SCENE_ID    = 2;

    private int curScene { get; set; } = 0;

    private void Awake()
    {
      if (ModulesCommon.SceneLoader == null)
        ModulesCommon.SceneLoader = this;
      else
        Destroy(gameObject);

      DontDestroyOnLoad(this);
      curScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void loadNextScene()
    {
      if (curScene == CLICKER_SCENE_ID)
        curScene = HUB_SCENE_ID;
      else
        ++curScene;

      SceneManager.LoadScene(curScene);
    }
  }
}