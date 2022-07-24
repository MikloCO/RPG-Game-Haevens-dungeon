using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioSource MusicSource;
    public AudioClip MusicClip;
    public AudioSource AmbiSource;
    public AudioClip ambiClip;

    // Start is called before the first frame update
    void Start()
    {
        PlaySound(MusicSource, MusicClip);
        PlaySound(AmbiSource, ambiClip);
    }

    private void PlaySound(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }


}
