using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.UI;
using RPG.Audio;
using RPG.Saving;
using RPG.Attributes;
using RPG.Stats;
using RPG.Utils;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform handTransform_R = null;
        [SerializeField] Transform handTransform_L = null;
        [SerializeField] Weapon defaultWeapon = null;


        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        float manaRefill = Mathf.Infinity;

        [SerializeField] float manaGenerationTime= 5f;
        LazyValue<Weapon> currentWeapon;

        private void Awake()
        {
            currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);
        }

        private void Start()
        {
            currentWeapon.ForceInit();
        }
        private Weapon SetupDefaultWeapon()
        {
            AttachWeapon(defaultWeapon);
            return defaultWeapon;
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            manaRefill += Time.deltaTime;

            UpdateMana();

            if (target == null)
            {
                return;
            }

            if (target.IsDead())
            {
                GetComponent<Animator>().ResetTrigger("attack");
                return;
            }

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
                GetComponent<Animator>().ResetTrigger("attack");
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will trigger the Hit() event.
                TriggerAttack();
                timeSinceLastAttack = 0;
                if (GetComponent<Mana>() != null)
                    GetComponent<Mana>().UseMana();
            }
        }

        private void UpdateMana()
        {
            if(manaRefill > manaGenerationTime)
            {
                manaRefill = 0;
                if (GetComponent<Mana>() != null)
                    GetComponent<Mana>().GenerateManaPoints();
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().SetTrigger("attack");
            
        }

        // Animation Event
        void Hit()
        {
            if (target == null) { return; }

            float damage =GetComponent<BaseStats>().GetStat(Stat.Damage);
            target.TakeDamage(gameObject, damage); //currentWeapon.GetWeaponDamage());
        }

        void Shoot()
        {
            if (target == null) { return; }
            if (currentWeapon.value.HasProjectile())
            {
                float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
                currentWeapon.value.LaunchProjectile(handTransform_R, handTransform_L, target, gameObject, damage);
            }
            else
            {
                Hit();
            }
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            
            Health targetToTest = combatTarget.GetComponent<Health>();
            return combatTarget != null && combatTarget != targetToTest.IsDead();
            
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.value.GetWeaponRange();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            target = null;
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Mover>().Cancel();
        }

        public IEnumerable<float> GetAdditiveModifier(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.value.GetWeaponDamage();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon.value = weapon;
            AttachWeapon(weapon);
        }

        private void AttachWeapon(Weapon weapon)
        {
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(handTransform_R, handTransform_L, animator);
        }

        public object CaptureState()
        {
            return currentWeapon.value.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if(stat == Stat.Damage)
            {
                yield return currentWeapon.value.GetPercentageBonus();
            }
        }
    }
}
