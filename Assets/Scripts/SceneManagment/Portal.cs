using RPG.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagment
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E

        }
                Fader fader;

        private void Awake()
        {
            fader = FindObjectOfType<Fader>(true);

        }


        [SerializeField] String SceneToLoad = "sandbox02";
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeouttime = 2f;
        [SerializeField] float fadeintime = 1f;
        [SerializeField] float fadewaittime = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            transform.parent = null;
            if(SceneToLoad.Equals(""))
            {
                Debug.LogError("Scene to load is not set / empty.");
                yield break;
            }
            
            fader.EnableFader();
            yield return fader.FadeOut(fadeouttime);
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();

            wrapper.Save();
            
            yield return SceneManager.LoadSceneAsync(SceneToLoad);

            PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerController.enabled = false;
            
            wrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

           
            wrapper.Save();

            yield return new WaitForSeconds(fadewaittime);
            yield return fader.FadeIn(fadeintime);
            playerController.enabled = true;

            fader.DisableFader();
            Destroy(gameObject);
            Destroy(fader);


        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }
            return null;
        }

        private void UpdatePlayer(Portal otherportal)
        {
            GameObject spawn = GameObject.Find("SpawnPoint");
            GameObject player = GameObject.FindWithTag("Player");
            otherportal.transform.position = spawn.transform.position;
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.GetComponent<NavMeshAgent>().transform.position = otherportal.transform.position;
            player.transform.rotation = spawn.transform.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
