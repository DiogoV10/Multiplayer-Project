using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    public class SpawnWeapon : MonoBehaviour
    {


        [SerializeField] private WeaponObjectSO weaponObjectSO;
        [SerializeField] private Transform counterTopPoint;


        private WeaponObject weaponObject;


        public void Interact()
        {
            if (weaponObjectSO != null)
            {
                Transform weaponObjectTransform = Instantiate(weaponObjectSO.prefab, counterTopPoint);
                weaponObjectTransform.GetComponent<WeaponObject>().SetSpawnWeapon(this);
            }
        }

        public Transform GetWeaponObjectFollowTransform()
        {
            return counterTopPoint;
        }

        public void SetWeaponObject(WeaponObject weaponObject)
        {
            this.weaponObject = weaponObject;
        }

        public WeaponObject GetWeaponObject() 
        { 
            return weaponObject; 
        }

        public void ClearWeaponObject()
        {
            weaponObject = null;
        }

        public bool HasWeaponObject()
        {
            return weaponObject != null;
        }


    }
}
