using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public void SetHealth(float healthpoints)
    {
        slider.value = healthpoints;
    }
}
