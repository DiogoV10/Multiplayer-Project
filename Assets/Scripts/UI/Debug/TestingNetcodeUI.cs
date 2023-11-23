using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using V10;

public class TestingNetcodeUI : MonoBehaviour
{


    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startClientButton;


    private void Awake()
    {
        startHostButton.onClick.AddListener(() =>
        {
            Debug.Log("Host");
            GameMultiplayer.Instance.StartHost();
            Hide();
        });
        
        startClientButton.onClick.AddListener(() =>
        {
            Debug.Log("Client");
            GameMultiplayer.Instance.StartClient();
            Hide();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }


}
