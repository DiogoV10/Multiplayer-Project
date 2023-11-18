using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    public class CinemachineCameraFollow : MonoBehaviour
    {


        public static CinemachineCameraFollow Instance { get; private set; }


        private CinemachineVirtualCamera cinemachineVirtualCamera;


        private void Awake()
        {
            Instance = this;

            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetFollow(Transform transform)
        {
            cinemachineVirtualCamera.Follow = transform;
        }


    }
}
