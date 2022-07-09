using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{

    public class PatrolPath : MonoBehaviour
    {
        [SerializeField]
        float spehereradius = 0.5f;
     
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(GetWayPoint(i), spehereradius);
                
                if(i + 1 < transform.childCount)
                {
                    Gizmos.DrawLine(GetWayPoint(i + 1), GetWayPoint(i));
                }
                if(i + 1 == transform.childCount)
                {
                    Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(0));
                }
            }
        }

        public int GetNextWayPoint(int index)
        {
            if(index + 1 == transform.childCount)
            {
                return 0;
            }
            return index + 1;
        }
        public Vector3 GetWayPoint(int index)
        {
            return gameObject.transform.GetChild(index).position;
        }
        public Vector3 GetWayPoint(int index, int increment)
        {  
            return gameObject.transform.GetChild(index+increment).position;
        }
    }
}
