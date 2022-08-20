using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayXP : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public TextMeshProUGUI scoreText;


    public void SetXPPoints(float xpPoints)
    {
        print(xpPoints);
        slider.value = xpPoints;
        scoreText.SetText(xpPoints.ToString());
    }
}
