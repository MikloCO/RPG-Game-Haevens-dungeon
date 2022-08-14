using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.UI;
using RPG.Audio;
using RPG.Saving;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform handTransform_R = null;
        [SerializeField] Transform handTransform_L = null;
        [SerializeField] Weapon defaultWeapon = null;


        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        float manaRefill = Mathf.Infinity;

        [SerializeField] float manaGenerationTime= 5f;
        Weapon currentWeapon = null;


        private void Start()
        {
            if (currentWeapon == null)
            EquipWeapon(defaultWeapon);
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
            target.TakeDamage(currentWeapon.GetWeaponDamage());

        }

        void Shoot()
        {
            if (target == null) { return; }
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(handTransform_R, handTransform_L, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.GetWeaponDamage());
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
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
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

        public void EquipWeapon(Weapon weapon)
        {
             
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(handTransform_R, handTransform_L, animator);
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}
