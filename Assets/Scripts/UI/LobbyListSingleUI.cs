using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

namespace V10
{
    public class LobbyListSingleUI : MonoBehaviour
    {


        [SerializeField] private TextMeshProUGUI lobbyNameText;


        private Lobby lobby;


        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                GameLobby.Instance.JoinWithId(lobby.Id);
            });
        }

        public void SetLobby(Lobby lobby)
        {
            this.lobby = lobby;
            lobbyNameText.text = lobby.Name;
        }


    }
}
