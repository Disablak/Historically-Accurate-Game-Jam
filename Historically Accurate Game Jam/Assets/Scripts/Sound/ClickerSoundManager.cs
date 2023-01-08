using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class ClickerSoundManager : MonoBehaviour
{
  [SerializeField] private AudioClip   pickaxeAudioClip;
  [SerializeField] private AudioClip   diamondAudioClip;
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioSource timeRunningOutAudioSource;


  public void playMinedSound(ResourceType resource_type, int amount, int total, bool is_helper)
  {
    if (!is_helper)
      audioSource.PlayOneShot(pickaxeAudioClip);
    if(resource_type == ResourceType.DIAMOND)
      audioSource.PlayOneShot(diamondAudioClip);
  }

  public void playTimer()
  {
  }
}
