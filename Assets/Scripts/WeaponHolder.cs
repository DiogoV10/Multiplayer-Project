using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class WeaponHolder : NetworkBehaviour
    {


        [SerializeField] private string defaultWeaponName;
        private Transform currentWeapon;


        private void Start()
        {
            DeactivateAllWeapons();
            ActivateDefaultWeapon();

            GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        }

        private void GameInput_OnInteractAction(object sender, System.EventArgs e)
        {
            if (!IsOwner) return;

            SwitchToNextWeaponServerRPC();
        }

        [ServerRpc(RequireOwnership = false)]
        private void SwitchToNextWeaponServerRPC()
        {
            SwitchToNextWeaponClientRPC();
        }

        [ClientRpc]
        private void SwitchToNextWeaponClientRPC()
        {
            SwitchToNextWeapon();
        }

        private void SwitchToNextWeapon()
        {
            DeactivateCurrentWeapon();

            int nextIndex = (currentWeapon.GetSiblingIndex() + 1) % transform.childCount;
            currentWeapon = transform.GetChild(nextIndex);

            ActivateCurrentWeapon();
        }

        private void DeactivateCurrentWeapon()
        {
            if (currentWeapon != null)
            {
                currentWeapon.gameObject.SetActive(false);
            }
        }

        private void ActivateCurrentWeapon()
        {
            if (currentWeapon != null)
            {
                currentWeapon.gameObject.SetActive(true);
            }
        }

        private void DeactivateAllWeapons()
        {
            foreach (Transform weapon in transform)
            {
                if (weapon.gameObject != null)
                {
                    weapon.gameObject.SetActive(false);
                }
            }
        }

        private void ActivateDefaultWeapon()
        {
            currentWeapon = transform.Find(defaultWeaponName);

            if (currentWeapon != null)
            {
                currentWeapon.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("Default weapon not found in WeaponHolder!");
            }
        }


    }
}
