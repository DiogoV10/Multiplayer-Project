using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    public class GameManager : MonoBehaviour
    {
        

        public static GameManager Instance { get; private set; }


        public event EventHandler OnStateChanged;
        public event EventHandler OnLocalPlayerReadyChanged;


        private enum State
        {
            WaitingToStart,
            CountdownToStart,
            GamePlaying,
            GameOver,
        }


        private State state;
        private bool isLocalPlayerReady;
        private float countdownToStartTimer = 3f;
        private float gamePlayingTimer;
        private float gamePlayingTimerMax = 5f;


        private void Awake()
        {
            Instance = this;

            //state = State.WaitingToStart;
            state = State.CountdownToStart;
        }

        private void Start()
        {
            GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        }

        private void GameInput_OnInteractAction(object sender, System.EventArgs e)
        {
            if (state == State.WaitingToStart)
            {
                isLocalPlayerReady = true;
                OnLocalPlayerReadyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Update()
        {
            switch (state)
            {
                case State.WaitingToStart:
                    break;
                case State.CountdownToStart:
                    countdownToStartTimer -= Time.deltaTime;
                    if (countdownToStartTimer < 0f)
                    {
                        state = State.GamePlaying;
                        gamePlayingTimer = gamePlayingTimerMax;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case State.GamePlaying:
                    gamePlayingTimer -= Time.deltaTime;
                    if (gamePlayingTimer < 0f)
                    {
                        state = State.GameOver;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case State.GameOver:
                    break;
            }

            Debug.Log(state);
        }

        public bool IsGamePlaying()
        {
            return state == State.GamePlaying;
        }
        
        public bool IsCountdownToStartActive()
        {
            return state == State.CountdownToStart;
        }

        public float GetCountdownToStartTimer()
        {
            return countdownToStartTimer;
        }

        public bool IsGameOver()
        {
            return state == State.GameOver;
        }

        public bool IsLocalPlayerReady()
        {
            return isLocalPlayerReady;
        }


    }
}
