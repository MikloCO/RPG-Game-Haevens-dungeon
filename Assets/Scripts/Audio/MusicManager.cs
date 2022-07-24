using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Audio
{

    // SX = Sound Effects, MX = Music, VX = Voice over / Dialouge
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] List<AudioClip> BackgroundMusic;
        private AudioSource audioSource_Backgroundmusic;


        void Awake()
        {
            audioSource_Backgroundmusic = GameObject.Find("AudioManagerBG").GetComponent<AudioSource>();

        }

        public void EnableCombatMusic()
        {
            //if(audioSource_Backgroundmusic.clip == BackgroundMusic[1])
            //{
            //    return;
            //}
            audioSource_Backgroundmusic.clip = BackgroundMusic[1];
            //audioSource_Backgroundmusic.PlayOneShot(BackgroundMusic[1]);
            
        }

        public void EnableSneakMusic()
        {
            //if (audioSource_Backgroundmusic.clip == BackgroundMusic[0])
            //{
            //    return;
            //}
            audioSource_Backgroundmusic.clip = BackgroundMusic[0];
            //audioSource_Backgroundmusic.PlayOneShot(BackgroundMusic[0]);
        }

        public void PlayMusic()
        {
            if(!audioSource_Backgroundmusic.isPlaying)
            {
                audioSource_Backgroundmusic.Play();
            }
        }
        public void StopMusic()
        {
            if(audioSource_Backgroundmusic.isPlaying)
            {
                audioSource_Backgroundmusic.Stop();
            }

        }
    }
}
