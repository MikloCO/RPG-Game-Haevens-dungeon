using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class Mana : MonoBehaviour
    {
        [SerializeField] float manaPoints = 100f;
        [SerializeField] float manaUsage = 10f;

        public ManaBar Manabar;
        float manageneration = Mathf.Infinity;

        public float GetMana() 
        {
            return manaPoints;
        }

        public void UseMana()
        {
            if (Manabar == null) return;
            manaPoints -= manaUsage;
            Manabar.SetMana(manaPoints);
        }

        public void GenerateManaPoints()
        {
            if(manaPoints >= 100f) { return; }
            manageneration += Time.deltaTime;
            manaPoints += 2.5f;
            Manabar.SetMana(manaPoints);
            if(manaPoints == 100f)
            {
                manageneration = 0;
            }
        }



    }
}