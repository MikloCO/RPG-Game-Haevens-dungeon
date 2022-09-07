using RPG.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
         float capsuleHeight = 2f;
        Health target = null;
        [SerializeField] float speed = 1f;
        float damage = 0f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 8f;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterDestroy = 0.2f;

        GameObject instigator = null;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;
        
            if(isHoming && !target.IsDead()) transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;

            Destroy(gameObject, maxLifeTime);
        }

        public  Vector3 GetAimLocation()
        {
            CapsuleCollider targetBody = target.GetComponent<CapsuleCollider>();
            if (targetBody == null) return target.transform.position;
            return target.transform.position + Vector3.up * targetBody.height / capsuleHeight;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            if (other.GetComponent<Health>().IsDead()) return;
            target.TakeDamage(gameObject, damage);
            target.TakeDamage(instigator, damage);

            //Health health = other.GetComponent<Health>();
            //if (target != null && health != target) return;
            //if (health == null || health.IsDead()) return;
            //if (other.gameObject == instigator) return;
            //health.TakeDamage(instigator, damage);


            speed = 0f;

            if(hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeAfterDestroy);


        }
    }
}
