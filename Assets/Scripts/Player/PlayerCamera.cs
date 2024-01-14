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
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        [SerializeField] private AudioListener audioListener;


        private void Start()
        {
            if (!IsOwner)
            {
                return;
            }

            Cursor.lockState = CursorLockMode.Locked;

            cinemachineVirtualCamera.GetComponent<CinemachineCameraFollow>().SetFollow(cameraFollow);
            cinemachineVirtualCamera.GetComponent<CinemachinePOVExtension>().SetPlayerTransform(transform);
        }

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                audioListener.enabled = true;
                cinemachineVirtualCamera.Priority = 1;
            }
            else
            {
                cinemachineVirtualCamera.Priority = 0;
            }
        }


    }
}
