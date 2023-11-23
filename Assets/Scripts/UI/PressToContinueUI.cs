using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    public class PressToContinueUI : MonoBehaviour
    {


        private void Start()
        {
            GameManager.Instance.OnLocalPlayerReadyChanged += GameManager_OnLocalPlayerReadyChanged;

            Show();
        }

        private void GameManager_OnLocalPlayerReadyChanged(object sender, System.EventArgs e)
        {
            if (GameManager.Instance.IsLocalPlayerReady())
            {
                Hide();
            }
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
