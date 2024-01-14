using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace V10
{
    public class Grenade : MonoBehaviour
    {
        public float delay = 3f;
        float countdown;

        public GameObject explosionEffect;

        public float damageRadius = 5f;

        bool hasExploded = false;

        void Start()
        {
            countdown = delay;
        }

        // Update is called once per frame
        void Update()
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f && !hasExploded)
            {
                hasExploded = true;
                Explode();
            }
        }

        void Explode()
        {
            GameObject effect = Instantiate(explosionEffect,transform.position,transform.rotation);

            Debug.Log(hasExploded);

            Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);

            foreach (Collider collider in colliders)
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable?.Damage(50);
                }
            }

            Destroy(effect, 2f);
            Destroy(gameObject);
        }
    }
}
