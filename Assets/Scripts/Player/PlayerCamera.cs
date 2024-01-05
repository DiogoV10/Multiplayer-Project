using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class PlayerCamera : NetworkBehaviour
    {


        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Transform cameraFollow;
        [SerializeField] private AudioListener audioListener;


        private CinemachineCameraFollow cinemachineCameraFollow;
        private CinemachinePOVExtension cinemachinePOVExtension;


        private void Awake()
        {
            cinemachineCameraFollow = virtualCamera.GetComponent<CinemachineCameraFollow>();
            cinemachinePOVExtension = virtualCamera.GetComponent<CinemachinePOVExtension>();
        }

        private void Start()
        {
            if (!IsOwner)
            {
                return;
            }

            Cursor.lockState = CursorLockMode.Locked;

            cinemachineCameraFollow.SetFollow(cameraFollow);
            cinemachinePOVExtension.SetPlayerTransform(transform);
        }

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                audioListener.enabled = true;
                virtualCamera.Priority = 1;
            }
            else
            {
                virtualCamera.Priority = 0;
            }
        }


    }
}
