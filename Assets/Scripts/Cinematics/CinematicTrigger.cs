using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{

    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        private bool CinematicSceneHasBeenTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if (!CinematicSceneHasBeenTriggered && (other.tag == "Player" || other.tag == "Avatar"))
            {
                GetComponent<PlayableDirector>().Play();
                CinematicSceneHasBeenTriggered = true;
            }
        }
        public object CaptureState()
        {
            return CinematicSceneHasBeenTriggered;
        }

        public void RestoreState(object state)
        {
            CinematicSceneHasBeenTriggered = (bool)state;
            
        }
    }
}
