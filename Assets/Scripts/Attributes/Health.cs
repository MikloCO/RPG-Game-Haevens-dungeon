using RPG.Combat;
using RPG.Control;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using RPG.Utils;
using UnityEditor;
using System;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70f;
        [SerializeField] UnityEvent takeDamage;
        public  UnityEngine.Events.UnityEvent StayWithMe = new UnityEngine.Events.UnityEvent();


        LazyValue<float> healthPoints;

        //[SerializeField] float healthPoints = 100f;

        public HealthBar healthbar;

        bool isDead = false;

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth); //LazyV will initialize healthpoints before usage.
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health); ;
        }

        private void Start()
        {
            //if(healthPoints < 0)
            //healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            healthPoints.ForceInit(); //force value initilization
       //     StayWithMe.Invoke();
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }
        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100f);
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);
        }

        public float GetHealth()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            try
            {

            StayWithMe.Invoke();
            }
            catch (Exception any)
            {
                print("WTF IS WRONG");
                throw;
            }

            print(gameObject.name + " took damage: " + damage);
            
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            takeDamage.Invoke();
            
            if (healthbar != null)
                healthbar.SetHealth(healthPoints.value);


            EliminateCharacter(instigator);
        }

        private void EvolveExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
            print(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
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
            if(healthbar != null && this.gameObject.transform.childCount == 2)
            {
                DisableHealthBar();
            }
            if(GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
            {
                GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            }
            if(GetComponentInChildren<Canvas>() != null)
            {
                GetComponentInChildren<Canvas>().enabled = false;
            }

        }

        private void DisableHealthBar()
        {
            GameObject healthbar = this.gameObject.transform.GetChild(1).gameObject;
            if (this.gameObject.transform.GetChild(1).gameObject != null && healthbar.name.Equals("EnemyCanvas"))
            {
                healthbar.SetActive(false);
            }
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;
            if (healthPoints.value == -1)
            {
                TriggerDeath();
                GetComponent<Animator>().ResetTrigger("die");
            }
        }

        private void EliminateCharacter(GameObject instigator)
        {
            if (healthPoints.value == -1)
            {
                GetComponent<Animator>().ResetTrigger("die");
                print(healthPoints);
                return;
            }
            if (healthPoints.value <= 0)
            {
                TriggerDeath();
                healthPoints.value = -1;
                EvolveExperience(instigator);
                print(healthPoints + instigator.name);
               

            }
        }
    }
}