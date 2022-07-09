using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{

    public class CinematicTrigger : MonoBehaviour
    {
        private bool CinematicSceneHasBeenTriggered = false;
        private void OnTriggerEnter(Collider other)
        {
            if(!CinematicSceneHasBeenTriggered && other.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                CinematicSceneHasBeenTriggered = true;
            }
        }
    }
}
