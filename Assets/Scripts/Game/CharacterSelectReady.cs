using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class CharacterSelectReady : NetworkBehaviour
    {


        public static CharacterSelectReady Instance { get; private set; }


        public event EventHandler OnReadyChanged;


        private Dictionary<ulong, bool> playerReadyDictionary;


        private void Awake()
        {
            Instance = this;

            playerReadyDictionary = new Dictionary<ulong, bool>();
        }

        public void SetPlayerReady()
        {
            SetPlayerReadyServerRPC();
        }

        [ServerRpc(RequireOwnership = false)]
        private void SetPlayerReadyServerRPC(ServerRpcParams serverRpcParams = default)
        {
            SetPlayerReadyClientRPC(serverRpcParams.Receive.SenderClientId);

            playerReadyDictionary[serverRpcParams.Receive.SenderClientId] = true;

            bool allClientsReady = true;

            foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
            {
                if (!playerReadyDictionary.ContainsKey(clientId) || !playerReadyDictionary[clientId])
                {
                    allClientsReady = false;
                    break;
                }
            }

            if (allClientsReady)
            {
                Loader.LoadNetwork(Loader.Scene.GameScene);
            }
        }

        [ClientRpc]
        private void SetPlayerReadyClientRPC(ulong clientId)
        {
            playerReadyDictionary[clientId] = true;

            OnReadyChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool IsPlayerReady(ulong clientId)
        {
            return playerReadyDictionary.ContainsKey(clientId) && playerReadyDictionary[clientId];
        }


    }
}
