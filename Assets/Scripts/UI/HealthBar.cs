using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Gradient gradient;

    public void SetHealth(float healthpoints)
    {
        slider.value = healthpoints;
        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    //public void SetMaxHealth(float healthpoints)
    //{
    //    slider.maxValue = healthpoints;
    //    slider.value = healthpoints;
    //}

}
