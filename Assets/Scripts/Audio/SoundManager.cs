using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public List<AudioClip> Footsteps;

    public AudioClip PickAudioClipToPlay(int index)
    {
        return Footsteps[index];
    }

    public void PlayAudioClipOneShot(AudioClip clipToPlay)
    {
        GetComponent<AudioSource>().PlayOneShot(clipToPlay);
    }

    public void PlayAudioClip(AudioClip clipToPlay)
    {
        GetComponent<AudioSource>().clip = clipToPlay;
        GetComponent<AudioSource>().Play();
    }


}
