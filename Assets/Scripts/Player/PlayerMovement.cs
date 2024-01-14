using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Windows;

namespace V10
{
    public class PlayerMovement : NetworkBehaviour
    {


        [Header("Movement Stats")]
        [SerializeField] private float speed = 4f;
        [SerializeField] private float sprintSpeed = 8f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float jumpHeight = 3f;
        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        [SerializeField] private float jumpTimeout = 0.50f;
        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        [SerializeField] private float fallTimeout = 0.15f;
        [Tooltip("Acceleration and deceleration")]
        [SerializeField] private float SpeedChangeRate = 10.0f;

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

        private float jumpTimeoutDelta;
        private float fallTimeoutDelta;
        private float animationMovementSpeedBlend;

        private bool canJump;

        private bool isGrounded;
        private bool isSprinting;
        private bool isJumping;
        private bool isFalling;


        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            GameInput.Instance.OnJumpAction += GameInput_OnJumpAction;
            GameInput.Instance.OnSprintAction += GameInput_OnSprintAction;

            jumpTimeoutDelta = jumpTimeout;
            fallTimeoutDelta = fallTimeout;
        }

        private void GameInput_OnSprintAction(object sender, System.EventArgs e)
        {
            //if (!GameManager.Instance.IsGamePlaying() && !GameManager.Instance.IsGameOver()) return;

            //if (GameManager.Instance.IsGamePaused()) return;

            isSprinting = !isSprinting;
        }

        private void GameInput_OnJumpAction(object sender, System.EventArgs e)
        {
            if (!GameManager.Instance.IsGamePlaying() && !GameManager.Instance.IsGameOver()) return;

            if (GameManager.Instance.IsGamePaused()) return;

            if (isGrounded)
            {
                canJump = true;
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

            HandleJump();
            HandleMovement();
        }

        private void HandleMovement()
        {
            bool isFeetGrounded = Physics.Raycast(groundCheck.position, -transform.up, groundDistance, groundMask);

            isGrounded = isFeetGrounded && (Vector3.Angle(Vector3.up, hitNormal) <= characterController.slopeLimit);

            bool hitCeiling = Physics.Raycast(ceilingCheck.position, Vector3.up, ceilingCheckDistance);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (hitCeiling && velocity.y > 0)
            {
                velocity.y = 0f;
            }

            Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

            if (GameManager.Instance.IsGamePaused())
            {
                inputVector = Vector2.zero;
            }

            float currentSpeed = isSprinting ? sprintSpeed : speed;

            if (inputVector == Vector2.zero)
            {
                currentSpeed = 0f;
            }

            animationMovementSpeedBlend = Mathf.Lerp(animationMovementSpeedBlend, currentSpeed, Time.deltaTime * SpeedChangeRate);
            if (animationMovementSpeedBlend < 0.01f) animationMovementSpeedBlend = 0f;

            float x = inputVector.x;
            float z = inputVector.y;

            Vector3 move = transform.right * x + transform.forward * z;

            characterController.Move(move * currentSpeed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            characterController.Move(velocity * Time.deltaTime);
        }

        private void HandleJump()
        {
            if (isGrounded)
            {
                fallTimeoutDelta = fallTimeout;

                isJumping = false;
                isFalling = false;

                if (velocity.y < 0.0f)
                {
                    velocity.y = -2f;
                }

                if (canJump && jumpTimeoutDelta <= 0.0f)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

                    isJumping = true;
                }

                if (jumpTimeoutDelta >= 0.0f)
                {
                    jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                jumpTimeoutDelta = jumpTimeout;

                if (fallTimeoutDelta >= 0.0f)
                {
                    fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    isFalling = true;
                }

                canJump = false;
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            hitNormal = hit.normal;
        }

        public bool IsJumping()
        {
            return isJumping;
        }

        public bool IsFalling()
        {
            return isFalling;
        }

        public bool IsGrounded()
        {
            return isGrounded;
        }

        public float Speed()
        {
            return animationMovementSpeedBlend;
        }


    }
}