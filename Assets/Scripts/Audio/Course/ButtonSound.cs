using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource ButtonSource;
  
    public void PlayButtonSound(AudioClip clip)
    {
        if(!ButtonSource.isPlaying)
        {
            ButtonSource.clip = clip;
            ButtonSource.Play();
        }
    }
}
