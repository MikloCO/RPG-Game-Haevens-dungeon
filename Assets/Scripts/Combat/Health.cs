using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);
            if(healthPoints == -1)
            {
                GetComponent<Animator>().ResetTrigger("die");
                print(healthPoints);
                return;
            }
            if(healthPoints == 0)
            {
                TriggerDeath();
                healthPoints =- 1;
            }

        }
        public void TriggerDeath()
        {
            isDead = true;
            GetComponent<Animator>().Play("Death_");
            GetComponent<CapsuleCollider>().enabled = false;

        
        }
    }
}
