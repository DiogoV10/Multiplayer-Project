using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
    public class GunDataSO : ScriptableObject
    {
        [Header("Shooting")]
        public float damage;
        public float maxDistance;
        public float bulletSpread;

        [Header("Reloading")]
        public int currentAmmo;
        public int magSize;
        public float fireRate;
        public float reloadTime;

        [Header("Bullet")]
        public float shootForce;
        public float recoilForce;

        [HideInInspector]
        public bool isReloading;
    }
}
