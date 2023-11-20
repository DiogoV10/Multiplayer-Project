using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class PlayerMovement : NetworkBehaviour
    {


        [Header("Movement Stats")]
        [SerializeField] private float speed = 4f;
        [SerializeField] private float sprintSpeed = 8f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float jumpHeight = 3f;

        [Header("Ground Settings")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.4f;
        [SerializeField] private LayerMask groundMask;

        [Header("Ceiling Settings")]
        [SerializeField] private Transform ceilingCheck;
        [SerializeField] private float ceilingCheckDistance = 0.2f;


        private CharacterController characterController;
        

        private Vector3 velocity;
        private Vector3 hitNormal;
        private bool isGrounded;
        private bool isSprinting;


        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            GameInput.Instance.OnJumpAction += GameInput_OnJumpAction;
            GameInput.Instance.OnSprintAction += GameInput_OnSprintAction;
        }

        private void GameInput_OnSprintAction(object sender, System.EventArgs e)
        {
            if (!GameManager.Instance.IsGamePlaying() && !GameManager.Instance.IsGameOver()) return;

            isSprinting = !isSprinting;
        }

        private void GameInput_OnJumpAction(object sender, System.EventArgs e)
        {
            if (!GameManager.Instance.IsGamePlaying() && !GameManager.Instance.IsGameOver()) return;

            if (isGrounded)
            {
                velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        private void Update()
        {
            if (!IsOwner)
            {
                return;
            }

            if (!GameManager.Instance.IsGamePlaying() && !GameManager.Instance.IsGameOver())
            {
                return;
            }

            bool isFeetGrounded = Physics.Raycast(groundCheck.position, -transform.up, groundDistance, groundMask);

            isGrounded = isFeetGrounded && (Vector3.Angle(Vector3.up, hitNormal) <= characterController.slopeLimit);

            bool hitCeiling = Physics.Raycast(ceilingCheck.position, Vector3.up, ceilingCheckDistance);

            if (isGrounded && velocity.y < 0 )
            {
                velocity.y = -2f;
            }

            if (hitCeiling && velocity.y > 0)
            {
                velocity.y = 0f;
            }

            Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

            float currentSpeed = isSprinting ? sprintSpeed : speed;

            float x = inputVector.x;
            float z = inputVector.y;

            Vector3 move = transform.right * x + transform.forward * z;

            characterController.Move(move * currentSpeed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            characterController.Move(velocity * Time.deltaTime);

        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            hitNormal = hit.normal;
        }


    }
}