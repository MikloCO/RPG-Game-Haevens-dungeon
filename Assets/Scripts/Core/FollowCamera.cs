using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    Transform Target;
    [SerializeField]
    int x;
    [SerializeField]
    int y;
    [SerializeField]
    int z;

    [SerializeField]
    int rx;
    [SerializeField]
    int ry;
    [SerializeField]
    int rz;

    void LateUpdate()
    {
        //Camera.main.transform.position = Target.up;

        transform.position = Target.transform.position + new Vector3(x,y, z);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x + rx, transform.eulerAngles.y + ry,
        transform.eulerAngles.z + rz);
    }
}
