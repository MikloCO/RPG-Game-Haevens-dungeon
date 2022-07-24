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
            DontDestroyOnLoad(gameObject);

            yield return fader.FadeOut(fadeouttime);
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
           
            wrapper.Save();
            
            yield return SceneManager.LoadSceneAsync(SceneToLoad);
            
            wrapper.Load();

            Portal otherPortal = GetOtherPortal();

            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadewaittime);
            yield return fader.FadeIn(fadeintime);
            Destroy(gameObject);
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

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject spawn = GameObject.Find("SpawnPoint");
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.GetComponent<NavMeshAgent>().Warp(spawn.transform.position);
            player.transform.rotation = spawn.transform.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
