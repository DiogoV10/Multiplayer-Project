using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class PlayerAnimator : NetworkBehaviour
    {


        private const string IS_JUMPING = "IsJumping";
        private const string IS_GROUNDED = "IsGrounded";
        private const string IS_FALLING = "IsFalling";
        private const string SPEED = "Speed";


        [SerializeField] private PlayerMovement playerMovement;


        private Animator animator;


        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (!IsOwner)
            {
                return;
            }

            animator.SetBool(IS_JUMPING, playerMovement.IsJumping());
            animator.SetBool(IS_GROUNDED, playerMovement.IsGrounded());
            animator.SetBool(IS_FALLING, playerMovement.IsFalling());
            animator.SetFloat(SPEED, playerMovement.Speed());
        }


    }
}
