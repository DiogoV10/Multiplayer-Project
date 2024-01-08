using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] public GunData gunData;
        void Start()
        {
            Destroy(gameObject, 2f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            damageable?.Damage(gunData.damage);
            Destroy(gameObject);
        }
    }
}
