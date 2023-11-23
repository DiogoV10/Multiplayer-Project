using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class Player : NetworkBehaviour
    {


        [SerializeField] private List<Vector3> spawnPositionList;
        

        public static Player LocalInstance { get; private set; }


        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                LocalInstance = this;
            }

            transform.position = spawnPositionList[GameMultiplayer.Instance.GetPlayerDataIndexFromClientId(OwnerClientId)];
        }


    }
}
