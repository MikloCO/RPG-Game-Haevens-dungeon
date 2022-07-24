using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Control
{
    public class ControlMinimap : MonoBehaviour
    {
        [SerializeField] Camera MinimapCamera;
        [SerializeField] float MaxZoom = 4f;
        [SerializeField] float MinZoom = 20f;
        [SerializeField] GameObject UIButtonZoomIn;
        [SerializeField] GameObject UIButtonZoomOut;


        public void IncreaseMinimapSize()
        {
            if (MinimapCamera == null) return;
            
            if (MinimapCamera.orthographicSize > MaxZoom)
            {
                UIButtonZoomOut.GetComponent<Button>().interactable = true;
                MinimapCamera.orthographicSize -= 1f;
            }
            if(MinimapCamera.orthographicSize == MaxZoom)
            {
                UIButtonZoomIn.GetComponent<Button>().interactable = false;
            }
        }
        public void DecreaseMinimapSize()
        {
            if (MinimapCamera == null) return;

            if (MinimapCamera.orthographicSize < MinZoom)
            {
                MinimapCamera.orthographicSize += 1f;
                UIButtonZoomIn.GetComponent<Button>().interactable = true;
            }
            if (MinimapCamera.orthographicSize == MinZoom)
            {
                UIButtonZoomOut.GetComponent<Button>().interactable = false;
            }
        }
    }
}
