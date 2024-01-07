using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class PlayerCamera : NetworkBehaviour
    {


        [Header("References")]
        [SerializeField] private Transform cameraFollow;


        private void Start()
        {
            if (!IsOwner)
            {
                return;
            }

            Cursor.lockState = CursorLockMode.Locked;

            CinemachineCameraFollow.Instance.SetFollow(cameraFollow);
            CinemachinePOVExtension.Instance.SetPlayerTransform(transform);
        }


    }
}
