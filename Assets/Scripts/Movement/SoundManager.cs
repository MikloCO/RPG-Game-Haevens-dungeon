using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Audio
{

    public class SoundManager : MonoBehaviour
    {

        //[SerializeField] List<AudioClip> Footsteps;
        //[SerializeField] List<AudioClip> Attack;

        //private AudioSource audioSource_SFX;



        //void Awake()
        //{
        //    audioSource_SFX = GameObject.Find("AudioManagerSFX").GetComponent<AudioSource>();
        //}



        //private AudioClip GetRandomClip(List<AudioClip> inquestion)
        //{
        //    int index = UnityEngine.Random.Range(0, Footsteps.Count - 1);
        //    return Footsteps[index];
        //}


        ////Unity Animation Events
        //void Step()
        //{
        //    if (!audioSource_SFX.isPlaying)
        //        audioSource_SFX.PlayOneShot(Footsteps[2]);
        //}

        //void RunStep()
        //{
        //    AudioClip clip = GetRandomClip(Footsteps);
        //    audioSource_SFX.PlayOneShot(clip);
        //}

        //void Hit()
        //{
        //    audioSource_SFX.PlayOneShot(Attack[0]);
        //}

        //void RunWereolf()
        //{
        //    audioSource_SFX.PlayOneShot(Footsteps[0]);
        //}



    }
}
