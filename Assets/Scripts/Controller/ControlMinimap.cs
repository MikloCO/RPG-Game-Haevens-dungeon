using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class ControlMinimap : MonoBehaviour
    {
        [SerializeField] Camera MinimapCamera;
        [SerializeField] float MaxZoom = 4f;
        [SerializeField] float MinZoom = 20f;

        public void IncreaseMinimapSize()
        {
            if (MinimapCamera.orthographicSize >= MaxZoom)
            {
                MinimapCamera.orthographicSize -= 1f;
            }
        }
        public void DecreaseMinimapSize()
        {
            if (MinimapCamera.orthographicSize <= MinZoom)
            {
                MinimapCamera.orthographicSize += 1f;
            }
        }
    }
}
