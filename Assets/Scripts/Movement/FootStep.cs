using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioClip;
    [SerializeField] List<AudioClip> audioClip02;
    private AudioSource audioSource;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Unity Animation Event
    private void Step()
    {
        //AudioClip clip = GetRandomClip(audioClip);
        //audioSource.PlayOneShot(clip);

    }

    private void RunStep()
    {
        AudioClip clip = GetRandomClip(audioClip02);
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip(List<AudioClip> inquestion)
    {
        int index = UnityEngine.Random.Range(0, audioClip.Count -1);
        return audioClip[index];
    }
}
