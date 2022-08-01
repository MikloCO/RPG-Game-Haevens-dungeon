using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateCamera : MonoBehaviour
{
    [SerializeField]
    float damping = 0.9f;


    public void Update()
    {
        if(Input.GetKey("q"))
        {
            var rotation = Vector3.up * damping;
            transform.Rotate(rotation , Space.World);
        }
        if (Input.GetKey("r"))
        {
            var rotation = Vector3.up * -damping;
            transform.Rotate(rotation, Space.World);
        }
    }
}
