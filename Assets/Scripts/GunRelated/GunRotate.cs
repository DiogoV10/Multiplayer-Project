using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace V10
{
    public class GunRotate : NetworkBehaviour
    {
        void Update()
        {
            if (!IsOwner)
            {
                return;
            }
            transform.SetParent(Camera.main.transform);
        }
    }
}
