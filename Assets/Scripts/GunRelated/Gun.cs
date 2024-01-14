using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] public GunDataSO gunData;
        [SerializeField] private Transform muzzle;
        //[SerializeField] private Camera targetCamera;
        [SerializeField] private GameObject bullet;

        float timeSinceLastShot;

        private void Awake()
        {
            gunData.currentAmmo = gunData.magSize;

        }

        private void OnEnable()
        {
            PlayerShoot.reloadInput += StartReload;
            PlayerShoot.shootInput += Shoot;
        }

        private void OnDisable()
        {
            PlayerShoot.reloadInput -= StartReload;
            PlayerShoot.shootInput -= Shoot;
        }

        private void Update()
        {
            timeSinceLastShot += Time.deltaTime;
            Debug.DrawRay(muzzle.position, muzzle.forward);
        }

        public bool CanShoot() => !gunData.isReloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

        public void Shoot()
        {
            if(gunData.currentAmmo > 0)
            {
                if(CanShoot())
                {
                    Vector3 bulletDirectionWithoutSpread = BulletDirection();
                    float spreadX = UnityEngine.Random.Range(-gunData.bulletSpread, gunData.bulletSpread);
                    float spreadY = UnityEngine.Random.Range(-gunData.bulletSpread, gunData.bulletSpread);

                    Vector3 bulletDirectionWithSpread = bulletDirectionWithoutSpread + new Vector3(spreadY, spreadX, 0);

                    Bullet(bulletDirectionWithSpread);
                    /*
                    if(Physics.Raycast(muzzle.position, transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
                    {
                        Debug.Log(hitInfo.transform.name);
                    }
                    */
                    gunData.currentAmmo--;
                    timeSinceLastShot = 0;
                    OnGunShot();
                    if (gunData.currentAmmo == 0) Debug.Log("No ammo left!");
                }
            }
        }

        void OnGunShot()
        {

        }

        public void StartReload()
        {
            if(!gunData.isReloading)
            {
                StartCoroutine(Reload());
            }
        }

        public IEnumerator Reload()
        {
            Debug.Log("Reloading...");
            gunData.isReloading = true;
            yield return new WaitForSeconds(gunData.reloadTime);
            gunData.currentAmmo = gunData.magSize;
            gunData.isReloading = false;
            Debug.Log("Reloading ended!");
        }

        private void Bullet(Vector3 directionWithSpread)
        {
            GameObject currentBullet = Instantiate(bullet, muzzle.position, Quaternion.identity);
            currentBullet.transform.forward = directionWithSpread.normalized;

            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * gunData.shootForce, ForceMode.Impulse);
        }



        private Vector3 BulletDirection()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
                targetPoint = hit.point;
            else
                targetPoint = ray.GetPoint(75);
            Vector3 directionWithoutSpread = targetPoint - muzzle.position;
            return directionWithoutSpread;
        }
    }
}
