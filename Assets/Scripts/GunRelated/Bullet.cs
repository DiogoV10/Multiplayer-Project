using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class Bullet : NetworkBehaviour
    {
        [SerializeField] public GunDataSO gunData;
        void Start()
        {
            if (IsServer)
            {
                StartCoroutine(DestroyBulletOnServer(gameObject.GetComponent<NetworkObject>()));
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out NetworkObject networkObject))
            {
                NetworkPlayerHealth healthComponent = networkObject.GetComponent<NetworkPlayerHealth>();

                if (IsServer && healthComponent != null)
                {
                    healthComponent.TakeDamage(gunData.damage);
                }
            }
        }

        private IEnumerator DestroyBulletOnServer(NetworkObject bullet)
        {
            yield return new WaitForSeconds(2f); // Change this to your desired delay
            bullet.Despawn();
        }
    }
}
