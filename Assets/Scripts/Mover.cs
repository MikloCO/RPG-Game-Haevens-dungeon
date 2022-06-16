using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    int playerSpeed = 2;


    // Start is called before the first frame update
    void Start()
    {
        
    }
   


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
       GetComponent<NavMeshAgent>().destination = target.position;
      
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if(hasHit)
        {
            target.position = hit.point;
        }
    }
}
