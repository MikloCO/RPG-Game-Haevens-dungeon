using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] GameObject popup;
        public void EscapeMenu()
        {
            popup.SetActive(false);
        }

        public void OpenMenu()
        {
            popup.SetActive(true);
        }
    }
}
