using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class GameMultiplayer : NetworkBehaviour
    {


        public const int MAX_PLAYER_AMOUNT = 11;
        

        public static GameMultiplayer Instance { get; private set; }


        private void Awake()
        {
            Instance = this;
        }

        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void StartClient()
        {
            NetworkManager.Singleton.StartClient();
        }


    }
}
