using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class MouseLook : MonoBehaviour
    {


        [Header("References")]
        [SerializeField] private Transform playerBody;

        [Header("Mouse Settings")]
        [SerializeField] private float mouseSensitivity = 100f;


        private float xRotation = 0f;


        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            Vector2 inputVector = GameInput.Instance.GetLookVector();

            float mouseX = inputVector.x * mouseSensitivity * Time.deltaTime;
            float mouseY = inputVector.y * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up, mouseX);
        }


    }
}