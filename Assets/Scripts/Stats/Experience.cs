using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0f;

        public event Action OnExperienceGained;

        private RectTransform repositionXPText;
        [SerializeField] GameObject XPTEXTING;

        private void Start()
        {
            repositionXPText = XPTEXTING.GetComponent<RectTransform>();
            if (repositionXPText != null) 
            XPTEXTING.GetComponent<TextMeshProUGUI>().text = experiencePoints.ToString();

            RepositionXPText();
        }

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            OnExperienceGained();
            print(experience);
            RepositionXPText();
        }
        public float GetExperiencePoints()
        {
            return experiencePoints;
        }

        private void RepositionXPText()
        {
            if (XPTEXTING == null) return;
            XPTEXTING.GetComponent<TextMeshProUGUI>().text = GetExperiencePoints().ToString();

            if (!gameObject.tag.Equals("Player"))
            {
                return;
            }

            if (experiencePoints < 10)
            {
                repositionXPText.localPosition = new Vector3(83f, -7.6f, 0);
            }
            else
            {
                repositionXPText.localPosition = new Vector3(83f, -7.6f, 0);
            }
        }


        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
            XPTEXTING.GetComponent<TextMeshProUGUI>().text = GetExperiencePoints().ToString();

        }
    }
}
