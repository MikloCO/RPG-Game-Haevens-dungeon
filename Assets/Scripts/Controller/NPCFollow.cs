using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Movement
{
    public class NPCFollow : MonoBehaviour
    {
        [SerializeField] GameObject Player;
        [SerializeField] float AllowedDistance = 5f;
        [SerializeField] float FollowSpeed = 2f;
        [SerializeField] Vector3 NPCSidePosition = new Vector3(2, 0, 0); //Position Npc to the right of the player.

        void Start()
        {
          
        }

        // Update is called once per frame
        void Update()
        {
            if (!(Vector3.Distance(transform.position, Player.transform.position) < AllowedDistance))
            {
                GetComponent<Mover>().MoveTo(Player.transform.position + NPCSidePosition, FollowSpeed);
            }
            else
            {
               // transform.LookAt(Player.transform);
                GetComponent<ActionScheduler>().CancelCurrentAction();
                GetComponent<Mover>().Cancel();
            }
            
        }
    }
}
