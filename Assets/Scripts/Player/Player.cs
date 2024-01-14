using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class Player : NetworkBehaviour, IWeaponObjectParent
    {


        [Header("Spawn Settings")]
        [SerializeField] private List<Vector3> spawnPositionList;

        [Header("Interaction Settings")]
        [SerializeField] private LayerMask countersLayerMask;
        [SerializeField] private Transform weaponObjectHoldPoint;


        private SpawnWeapon selectedSpawnWeapon;
        private WeaponObject weaponObject;


        public static Player LocalInstance { get; private set; }


        private void Start()
        {
            GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        }

        private void GameInput_OnInteractAction(object sender, System.EventArgs e)
        {
            if (selectedSpawnWeapon != null)
            {
                selectedSpawnWeapon.Interact(this);
            }
        }

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                LocalInstance = this;
            }

            transform.position = spawnPositionList[GameMultiplayer.Instance.GetPlayerDataIndexFromClientId(OwnerClientId)];
        }

        private void Update()
        {
            if (!IsOwner)
            {
                return;
            }

            if (!GameManager.Instance.IsGamePlaying() && !GameManager.Instance.IsGameOver())
            {
                return;
            }

            HandleInteractions();
        }

        private void HandleInteractions()
        {
            float interactDistance = 2f;
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance, countersLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out SpawnWeapon spawnWeapon))
                {
                    if (spawnWeapon != selectedSpawnWeapon)
                    {
                        selectedSpawnWeapon = spawnWeapon;
                    }
                }
                else
                {
                    selectedSpawnWeapon = null;
                }
            }
            else
            {
                selectedSpawnWeapon = null;
            }
        }

        public Transform GetWeaponObjectFollowTransform()
        {
            return weaponObjectHoldPoint;
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
            return NetworkObject;
        }


    }
}
