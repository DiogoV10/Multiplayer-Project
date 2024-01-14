using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

namespace V10
{
    public class LobbyUI : MonoBehaviour
    {


        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button createLobbyButton;
        [SerializeField] private Button quickJoinButton;
        [SerializeField] private Button joinCodeButton;
        [SerializeField] private Button costumizationScene;
        [SerializeField] private Button loadoutEnterScene;
        [SerializeField] private TMP_InputField joinCodeInputField;
        [SerializeField] private TMP_InputField playerNameInputField;
        [SerializeField] private LobbyCreateUI lobbyCreateUI;
        [SerializeField] private Transform lobbyContainer;
        [SerializeField] private Transform lobbyTemplate;


        private void Awake()
        {
            mainMenuButton.onClick.AddListener(() =>
            {
                GameLobby.Instance.LeaveLobby();
                Loader.Load(Loader.Scene.MainMenuScene);
            });

            createLobbyButton.onClick.AddListener(() =>
            {
                lobbyCreateUI.Show();
            });
            
            quickJoinButton.onClick.AddListener(() =>
            {
                GameLobby.Instance.QuickJoin();
            });

            joinCodeButton.onClick.AddListener(() =>
            {
                GameLobby.Instance.JoinWithCode(joinCodeInputField.text);
            });

            costumizationScene.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.CharacterCostumization);
            });

            loadoutEnterScene.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.WeaponsLoadout);
            });
            lobbyTemplate.gameObject.SetActive(false);
            
        }

        private void Start()
        {
            playerNameInputField.text = GameMultiplayer.Instance.GetPlayerName();
            playerNameInputField.onValueChanged.AddListener((string newText) =>
            {
                GameMultiplayer.Instance.SetPlayerName(newText);
            });

            GameLobby.Instance.OnLobbyListChanged += GameLobby_OnLobbyListChanged;
            UpdateLobbyList(new List<Lobby>());
        }

        private void GameLobby_OnLobbyListChanged(object sender, GameLobby.OnLobbyListChangedEventArgs e)
        {
            UpdateLobbyList(e.lobbyList);
        }

        private void UpdateLobbyList(List<Lobby> lobbyList)
        {
            foreach (Transform child in lobbyContainer)
            {
                if (child == lobbyTemplate) continue;
                Destroy(child.gameObject);
            }

            foreach (Lobby lobby in lobbyList)
            {
                Transform lobbyTransform = Instantiate(lobbyTemplate, lobbyContainer);
                lobbyTransform.gameObject.SetActive(true);
                lobbyTransform.GetComponent<LobbyListSingleUI>().SetLobby(lobby);
            }
        }

        private void OnDestroy()
        {
            GameLobby.Instance.OnLobbyListChanged -= GameLobby_OnLobbyListChanged;
        }


    }
}
