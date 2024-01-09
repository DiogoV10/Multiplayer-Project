using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public interface IWeaponObjectParent
    {
        public Transform GetWeaponObjectFollowTransform();

        public void SetWeaponObject(WeaponObject weaponObject);

        public WeaponObject GetWeaponObject();

        public void ClearWeaponObject();

        public bool HasWeaponObject();

        public NetworkObject GetNetworkObject();
    }
}
