using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace V10
{
    public class ExitLoadout : MonoBehaviour
    {
        Button exitButton;

        public GameObject thingToNotDestroy;

        private void Awake()
        {
            exitButton = GetComponent<Button>();
            exitButton.onClick.AddListener(OnPressExitButton);
        }

        void OnPressExitButton()
        {
            Loader.Load(Loader.Scene.LobbyScene);
        }
    }
}
