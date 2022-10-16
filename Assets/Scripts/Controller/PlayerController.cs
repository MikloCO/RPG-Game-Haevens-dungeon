using System;
using RPG.Attributes;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        Health health;


        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavMeshProjectionDistance = 1f;
        [SerializeField] float maxNavPathLength = 40f;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Vector2 hostspot;
            public Texture2D texture;
        }

        private void Awake()
        {
            health = GetComponent<Health>();
        }


        private void Update()
        {
            if (InteractWithUI()) return;
            if (health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            }
            if (InteractWithComponent()) return;

            if (InteractWithMovement()) return;

            SetCursor(CursorType.None);
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RayCastAllSorted();
            foreach (RaycastHit hit in hits) {
             IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if(raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] RayCastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        private bool InteractWithUI()
        {
            SetCursor(CursorType.UI);
            return EventSystem.current.IsPointerOverGameObject();
        }

        //private bool InteractWithCombat()
        //{
        //    RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        //    foreach (RaycastHit hit in hits)
        //    {
        //        CombatTarget target = hit.transform.GetComponent<CombatTarget>();
        //        if (target == null) continue;

        //        if (Input.GetMouseButton(0))
        //        {
        //            GetComponent<Fighter>().Attack(target.gameObject);
        //        }
        //        SetCursor(CursorType.Combat);
        //        return true;
        //    }
        //    return false;
        //}

        private bool InteractWithMovement()
        {

            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);
            bool isOverUI = EventSystem.current.IsPointerOverGameObject();

            if (hasHit && !isOverUI)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(target, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHit) return false;
            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(hit.point, out navMeshHit, 
                maxNavMeshProjectionDistance, NavMesh.AllAreas);
            if (!hasCastToNavMesh) return false;

            target = navMeshHit.position;

            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            if(path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > maxNavPathLength) return false;

            return true;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float totalCornersLength = 0f;
            if (path.corners.Length < 2) return totalCornersLength;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                totalCornersLength += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }    
            return totalCornersLength;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hostspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}