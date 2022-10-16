using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        //Called by unity animation event
        public void DestroyText()
        {
            Destroy(gameObject);
        }
    }
}
