using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageFade : MonoBehaviour {

  // the image you want to fade, assign in inspector
  public Image img;

  const float TIME = 0.5f;

  public void Start()
  {
    enableFade( true );
  }

  public void enableFade(bool enable)
  {
    // fades the image out when you click
    StartCoroutine(FadeImage(enable));
  }

  IEnumerator FadeImage(bool fadeAway)
  {
    // fade from opaque to transparent
    if (fadeAway)
    {
      // loop over 1 second backwards
      for (float i = TIME; i >= 0; i -= Time.deltaTime)
      {
        // set color with i as alpha
        img.color = new Color(0, 0, 0, i / TIME);
        yield return null;
      }
    }
    // fade from transparent to opaque
    else
    {
      // loop over 1 second
      for (float i = 0; i <= TIME; i += Time.deltaTime)
      {
        // set color with i as alpha
        img.color = new Color(0, 0, 0 , i / TIME);
        yield return null;
      }
    }
  }
}