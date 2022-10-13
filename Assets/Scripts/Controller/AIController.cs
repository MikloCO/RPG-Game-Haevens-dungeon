using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] 
        float chaseDistance = 5f;
        [SerializeField] 
        float suspicionTime = 3f;
        [SerializeField]
        PatrolPath patrolpath = null;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 2f;
        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;

        int currentWaypointIndex = 0;

        Fighter fighter;
        GameObject player;
        Mover mover;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float dwellingTime = Mathf.Infinity;

        LazyValue<Vector3> guardPosition;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
            guardPosition = new LazyValue<Vector3>(GetGuardPosition);
        
        }

        private Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        private void Start()
        {
            guardPosition.ForceInit();
        }

        private void Update()
        {

            if (InAttackRangeOfPlayer() && fighter != null && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            dwellingTime += Time.deltaTime;
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();

        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition.value;

            if(patrolpath != null)
            {
                if(AtWayPoint())
                {
                    dwellingTime = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWayPoint();
            }
            if(dwellingTime > waypointDwellTime)
            {
                if(fighter != null)
                {
                    fighter.Cancel();
                    mover.MoveTo(nextPosition, patrolSpeedFraction);
                }
            }
        }

        private Vector3 GetCurrentWayPoint()
        {
            return patrolpath.GetWayPoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            dwellingTime += Time.deltaTime;
            currentWaypointIndex = patrolpath.GetNextWayPoint(currentWaypointIndex);
        }

        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWayPoint < waypointTolerance;
        }

        private bool InAttackRangeOfPlayer()
        {
            return Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
        }

        //Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

    }
    
}
