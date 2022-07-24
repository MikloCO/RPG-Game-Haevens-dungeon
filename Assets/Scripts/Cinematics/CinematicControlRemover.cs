using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }

        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl; 
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }
        void DisableControl(PlayableDirector pd)
        {
           
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            if (player.GetComponent<PlayerController>() != null)
            {
                player.GetComponent<PlayerController>().enabled = false;
            }
        }

        void EnableControl(PlayableDirector pd)
        {
            if (player.GetComponent<PlayerController>() != null)
            {
                player.GetComponent<PlayerController>().enabled = true;
            }
        }
    }
}