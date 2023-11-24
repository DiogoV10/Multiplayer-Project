using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class GameManager : NetworkBehaviour
    {
        

        public static GameManager Instance { get; private set; }


        public event EventHandler OnStateChanged;
        public event EventHandler OnGamePaused;
        public event EventHandler OnGameUnpaused;
        public event EventHandler OnLocalPlayerReadyChanged;


        private enum State
        {
            WaitingToStart,
            CountdownToStart,
            GamePlaying,
            GameOver,
        }


        [SerializeField] private Transform playerPrefab;


        private NetworkVariable<State> state = new NetworkVariable<State>(State.WaitingToStart);
        private bool isLocalPlayerReady;
        private NetworkVariable<float> countdownToStartTimer = new NetworkVariable<float>(3f);
        private NetworkVariable<float> gamePlayingTimer = new NetworkVariable<float>(0f);
        private float gamePlayingTimerMax = 50f;
        private bool isGamePaused = false;
        private Dictionary<ulong, bool> playerReadyDictionary;


        private void Awake()
        {
            Instance = this;

            playerReadyDictionary = new Dictionary<ulong, bool>();
        }

        private void Start()
        {
            GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
            GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        }

        public override void OnNetworkSpawn()
        {
            state.OnValueChanged += State_OnValueChanged;

            if (IsServer)
            {
                NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManager_OnLoadEventCompleted;
            }
        }

        private void SceneManager_OnLoadEventCompleted(string sceneName, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
        {
            foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
            {
                Transform playerTransform = Instantiate(playerPrefab);
                playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
            }
        }

        private void State_OnValueChanged(State previousValue, State newValue)
        {
            OnStateChanged?.Invoke(this, EventArgs.Empty);


        }

        private void GameInput_OnPauseAction(object sender, EventArgs e)
        {
            TogglePauseGame();
        }

        private void GameInput_OnInteractAction(object sender, System.EventArgs e)
        {
            if (state.Value == State.WaitingToStart)
            {
                isLocalPlayerReady = true;
                OnLocalPlayerReadyChanged?.Invoke(this, EventArgs.Empty);

                SetPlayerReadyServerRpc();
            }
        }

        [ServerRpc(RequireOwnership = false)]
        private void SetPlayerReadyServerRpc(ServerRpcParams serverRpcParams = default)
        {
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
                state.Value = State.CountdownToStart;
            }
        }

        private void Update()
        {
            if (!IsServer)
            {
                return;
            }

            switch (state.Value)
            {
                case State.WaitingToStart:
                    break;
                case State.CountdownToStart:
                    countdownToStartTimer.Value -= Time.deltaTime;
                    if (countdownToStartTimer.Value < 0f)
                    {
                        state.Value = State.GamePlaying;
                        gamePlayingTimer.Value = gamePlayingTimerMax;
                    }
                    break;
                case State.GamePlaying:
                    gamePlayingTimer.Value -= Time.deltaTime;
                    if (gamePlayingTimer.Value < 0f)
                    {
                        state.Value = State.GameOver;
                    }
                    break;
                case State.GameOver:
                    break;
            }

            Debug.Log(state);
        }

        public bool IsGamePlaying()
        {
            return state.Value == State.GamePlaying;
        }
        
        public bool IsCountdownToStartActive()
        {
            return state.Value == State.CountdownToStart;
        }

        public float GetCountdownToStartTimer()
        {
            return countdownToStartTimer.Value;
        }

        public bool IsGameOver()
        {
            return state.Value == State.GameOver;
        }

        public bool IsWaitingToStart()
        {
            return state.Value == State.WaitingToStart;
        }

        public bool IsGamePaused()
        {
            return isGamePaused;
        }

        public bool IsLocalPlayerReady()
        {
            return isLocalPlayerReady;
        }

        public float GetGamePlayingTimerNormalized()
        {
            return 1 - gamePlayingTimer.Value / gamePlayingTimerMax;
        }

        public void TogglePauseGame()
        {
            isGamePaused = !isGamePaused;

            if (isGamePaused)
            {
                OnGamePaused?.Invoke(this, EventArgs.Empty);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                OnGameUnpaused?.Invoke(this, EventArgs.Empty);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }


    }
}
