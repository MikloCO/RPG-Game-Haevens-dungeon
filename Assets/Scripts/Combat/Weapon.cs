using RPG.Attributes;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG Project/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animWeaponOverride = null;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] float weaponRange = 5f;
        [SerializeField] float weaponDamage = 20f;
        [SerializeField] float percentageBonus = 0;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";

        public void Spawn(Transform handTransform_R, Transform handTransform_L, Animator animator)
        {
            DestroyOldWeapon(handTransform_R, handTransform_L);
            if (weaponPrefab != null)
            {
                Transform handTransform = GetTransform(handTransform_R, handTransform_L);
                GameObject weapon = Instantiate(weaponPrefab, handTransform);
                weapon.name = weaponName;
            }
            
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animWeaponOverride != null)
            {
                animator.runtimeAnimatorController = animWeaponOverride;
            }

            else if(overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }
        

        private void DestroyOldWeapon(Transform handTransform_R, Transform handTransform_L)
        {
            Transform oldWeapon = handTransform_R.Find(weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = handTransform_L.Find(weaponName);
            }
            if (oldWeapon == null) return;
            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetTransform(Transform handTransform_R, Transform handTransform_L)
        {
            Transform handTransform;
            if (isRightHanded)
                handTransform = handTransform_R;
            else handTransform = handTransform_L;
            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
        }

        public float GetWeaponRange()
        {
            return weaponRange;
        }

        public float GetPercentageBonus()
        {
            return percentageBonus;
        }

        public float GetWeaponDamage()
        {
            return weaponDamage;
        }
    
    }
}
