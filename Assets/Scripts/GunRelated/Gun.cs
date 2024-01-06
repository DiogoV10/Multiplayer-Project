using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] public GunData gunData;
        [SerializeField] private Transform muzzle;


        float timeSinceLastShot;

        private void Awake()
        {
            gunData.currentAmmo = gunData.magSize;

        }

        private void OnEnable()
        {
            PlayerShoot.reloadInput += Reload;
            PlayerShoot.shootInput += Shoot;
        }

        private void OnDisable()
        {
            PlayerShoot.reloadInput -= Reload;
            PlayerShoot.shootInput -= Shoot;
        }

        private void Update()
        {
            timeSinceLastShot += Time.deltaTime;
        }

        public bool CanShoot() => !gunData.isReloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

        public void Shoot()
        {
            if(gunData.currentAmmo > 0)
            {
                if(CanShoot())
                {
                    if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
                    {
                        Debug.Log(hitInfo.transform.name);
                    }
                    gunData.currentAmmo--;
                    timeSinceLastShot = 0;
                    OnGunShot();
                }
            }
        }

        void OnGunShot()
        {

        }

        public void Reload()
        {

        }
    }
}
