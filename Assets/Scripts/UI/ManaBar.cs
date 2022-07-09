using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class ManaBar : MonoBehaviour
    {
        public Slider slider;
        public Image fill;
        public Gradient gradient;

        public void SetMana (float manaPoints)
        {
            slider.value = manaPoints;
        }

        public void SetMaxMana(float manaPoints)
        {
            slider.maxValue = manaPoints;
            slider.value = manaPoints;
        }
    }
}