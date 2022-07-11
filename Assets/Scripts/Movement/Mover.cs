using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent navMeshAgent;
        [SerializeField] float maxSpeed = 6f;
        SoundManager sm;

        private void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
            sm = GetComponentInChildren<SoundManager>();
        }

        void Update()
        {
            UpdateAnimator();

        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            if (navMeshAgent != null && navMeshAgent.enabled)
            {
                navMeshAgent.destination = destination;
                navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
                navMeshAgent.isStopped = false;
            }
        }

        public void Cancel()
        {
            if(navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

   
    }
}