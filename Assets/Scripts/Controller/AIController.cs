using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        // Update is called once per frame
        void Update()
        {
            GameObject.FindWithTag("Player");
        }
    }
}
