using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class WeaponObject : NetworkBehaviour
    {


        [SerializeField] private WeaponObjectSO weaponObjectSO;


        private IWeaponObjectParent weaponObjectParent;

        private Vector3 initialPosition;


        private void Awake()
        {
            initialPosition = transform.position;
        }

        public WeaponObjectSO GetWeaponObjectSO()
        {
            return weaponObjectSO;
        }

        public void SetWeaponObjectParent(IWeaponObjectParent weaponObjectParent) 
        {
            if (this.weaponObjectParent != null)
            {
                this.weaponObjectParent.ClearWeaponObject();
            }

            this.weaponObjectParent = weaponObjectParent;

            if (weaponObjectParent.HasWeaponObject())
            {
                Debug.LogError("Already has object!");
            }

            weaponObjectParent.SetWeaponObject(this);

            transform.parent = weaponObjectParent.GetWeaponObjectFollowTransform();
            transform.localPosition = initialPosition;
            transform.localRotation = Quaternion.identity;
        }

        public IWeaponObjectParent GetWeaponObjectParent()
        {
            return weaponObjectParent;
        }

        public void DestroySelf()
        {
            weaponObjectParent.ClearWeaponObject();
            Destroy(gameObject);
        }



        public static void SpawnWeaponObject(WeaponObjectSO weaponObjectSO, IWeaponObjectParent weaponObjectParent)
        {
            GameMultiplayer.Instance.SpawnWeaponObject(weaponObjectSO, weaponObjectParent);
        }


    }
}
