using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    public class CinemachineCameraFollow : MonoBehaviour
    {


        private CinemachineVirtualCamera cinemachineVirtualCamera;


        private void Awake()
        {
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetFollow(Transform transform)
        {
            cinemachineVirtualCamera.Follow = transform;
        }


    }
}
