using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace V10
{
    public class GameOverUI : MonoBehaviour
    {


        [SerializeField] private TextMeshProUGUI killCountText;
        [SerializeField] private Button mainMenuButton;


        private void Awake()
        {
            mainMenuButton.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.Shutdown();
                Loader.Load(Loader.Scene.MainMenuScene);
            });
        }

        private void Start()
        {
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

            Hide();
        }

        private void GameManager_OnStateChanged(object sender, System.EventArgs e)
        {
            if (GameManager.Instance.IsGameOver())
            {
                Show();

                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Hide();
            }
        }

        private void Update()
        {
            killCountText.text = "0";
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }


    }
}
