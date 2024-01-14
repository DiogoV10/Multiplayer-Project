using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace V10
{
    public class WeaponButton : MonoBehaviour
    {
        public Loadout loadoutSystem;

        AssignWeaponText gun;

        private void Start()
        {
            Button weaponButton = GetComponent<Button>();
            weaponButton.onClick.AddListener(OnClickWeapon);
            gun = GetComponent<AssignWeaponText>();
        }

        private void OnClickWeapon()
        {
            Debug.Log(gun.gunToGetInfo.name);
            loadoutSystem.OnClickWeapon(gun.gunToGetInfo);
        }
    }
}
