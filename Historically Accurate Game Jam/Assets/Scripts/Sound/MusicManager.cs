using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip   hubAudioClip;
  [SerializeField] private AudioClip   clickerAudioClip;
  [SerializeField] private AudioClip   cartAudioClip;

  public void Awake()
  {
    if (ModulesCommon.musicManager == null)
      ModulesCommon.musicManager = this;
    else
      Destroy(gameObject);

    DontDestroyOnLoad(this);
  }

  private void Start()
  {
    playSceneMusic(ModulesCommon.SceneLoader.curScene);
  }

  public void playSceneMusic(int scene_index)
  {
    switch (scene_index)
    {
      case SceneLoader.HUB_SCENE_ID: playMusic(hubAudioClip); break;
      case SceneLoader.CLICKER_SCENE_ID: playMusic(clickerAudioClip); break;
      case SceneLoader.CART_SCENE_ID: playMusic(cartAudioClip); break;
    }
  }

  private void playMusic(AudioClip audio_clip)
  {
    audioSource.Stop();
    audioSource.clip = audio_clip;
    audioSource.Play();
    audioSource.loop = true;
  }
}
