using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    public class WeaponObject : MonoBehaviour
    {


        [SerializeField] private WeaponObjectSO weaponObjectSO;


        private SpawnWeapon spawnWeapon;


        public WeaponObjectSO GetWeaponObjectSO()
        {
            return weaponObjectSO;
        }

        public void SetSpawnWeapon(SpawnWeapon spawnWeapon) 
        {
            if (this.spawnWeapon != null)
            {
                this.spawnWeapon.ClearWeaponObject();
            }

            this.spawnWeapon = spawnWeapon;

            if (spawnWeapon.HasWeaponObject())
            {
                Debug.LogError("Already has object!");
            }

            spawnWeapon.SetWeaponObject(this);

            transform.parent = spawnWeapon.GetWeaponObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }

        public SpawnWeapon GetSpawnWeapon()
        {
            return spawnWeapon;
        }


    }
}
