using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class NetworkPlayerHealth : NetworkBehaviour
    {


        [SerializeField] private float startingHealth = 100;
        [SerializeField] private NetworkVariable<float> currentHealth = new NetworkVariable<float>();

        private void Start()
        {
            if (IsServer)
            {
                currentHealth.Value = startingHealth;
            }

            // Hook up the synchronization callback
            currentHealth.OnValueChanged += UpdateClientHealth;
        }

        public void TakeDamage(float damage)
        {
            if (!IsServer)
                return;

            currentHealth.Value -= damage;

            if (currentHealth.Value <= 0)
            {
                Die();
            }
        }

        private void UpdateClientHealth(float oldHealth, float newHealth)
        {
            if (!IsOwner) return;
        }

        private void Die()
        {
            if (IsServer)
            {
            }

            if (IsOwner)
            {
            }
        }


    }
}
