using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class SpawnWeapon : MonoBehaviour, IWeaponObjectParent
    {


        [SerializeField] private WeaponObjectSO weaponObjectSO;
        [SerializeField] private Transform counterTopPoint;


        private WeaponObject weaponObject;


        public void Interact(Player player)
        {
            if (player.HasWeaponObject())
            {
                // Player is carrying something
            }
            else
            {
                // Player is not carrying something
                WeaponObject.SpawnWeaponObject(weaponObjectSO, player);
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

        public NetworkObject GetNetworkObject()
        {
            return null;
        }


    }
}
