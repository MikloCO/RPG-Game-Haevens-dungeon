using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    public Transform cam;

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.LookAt(transform.position + cam.position);
        //transform.LookAt(cam.position);
        transform.LookAt(Camera.main.transform);
    }
}