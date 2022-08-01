using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour
{
    bool GateIsOpen = false;
    private void OnMouseDown()
    {
        if(!GateIsOpen)
        {
            GetComponent<Animator>().SetTrigger("gateOpen");
            GateIsOpen = true;
        }
        else
        {
            GetComponent<Animator>().ResetTrigger("gateOpen");
            GateIsOpen = false;

        }
    }

}
