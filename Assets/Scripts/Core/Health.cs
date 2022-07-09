using RPG.Combat;
using RPG.Control;
using RPG.Movement;
using UnityEngine;


namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        public HealthBar healthbar;

        bool isDead = false;

        public float GetHealth()
        {
            return healthPoints;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthbar != null)
            healthbar.SetHealth(healthPoints);
            //print(healthPoints);
            if (healthPoints == -1)
            {
                GetComponent<Animator>().ResetTrigger("die");
                print(healthPoints);
                return;
            }
            if (healthPoints == 0)
            {
                TriggerDeath();
                healthPoints = -1;
            }
        }
        public void TriggerDeath()
        {
            isDead = true;
            GetComponent<Animator>().Play("Death_");
            DisposeGameObject();
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void DisposeGameObject()
        {
            if (GetComponent<CapsuleCollider>() != null)
            {
                GetComponent<CapsuleCollider>().enabled = false;
            }
            if (GetComponent<AIController>() != null)
            {
                GetComponent<AIController>().enabled = false;
            }
            if (GetComponent<PlayerController>() != null)
            {
                GetComponent<PlayerController>().enabled = false;
            }
            if (GetComponent<Fighter>() != null)
            {
                GetComponent<Fighter>().enabled = false;
            }
            if(this.gameObject.transform.childCount == 2)
            {
                DisableHealthBar();
            }
            if(GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
            {
                GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            }
        }

        private void DisableHealthBar()
        {
            GameObject healthbar = this.gameObject.transform.GetChild(1).gameObject;
            if (healthbar != null && healthbar.name.Equals("EnemyCanvas"))
            {
                healthbar.SetActive(false);
            }
        }
    }

}