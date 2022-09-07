using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0f;
        private GameObject xpText;

        private RectTransform repositionXPText;
        [SerializeField] GameObject XPTEXTING;

        private void Start()
        {
            repositionXPText = XPTEXTING.GetComponent<RectTransform>();
            //UpdateUIXP();
            //RepositionXPText();
        }

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            //UpdateUIXP();
            //RepositionXPText();
        }
        public float GetExperiencePoints()
        {
            return experiencePoints;
        }

        private void RepositionXPText()
        {
            if (!gameObject.tag.Equals("Player")) return;
            if (XPTEXTING == null) return;

            if (experiencePoints < 10)
            {
                repositionXPText.localPosition = new Vector3(92f, -7.6f, 0);
            }
            else
            {
                repositionXPText.localPosition = new Vector3(83f, -7.6f, 0);
            }

            xpText.GetComponent<TextMeshProUGUI>().text = experiencePoints.ToString();
        }


        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }

        public void UpdateUIXP()
        {
            if (XPTEXTING == null) return;
            XPTEXTING.GetComponent<TextMeshProUGUI>().text = experiencePoints.ToString();

        }
    }
}
