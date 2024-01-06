using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    public class PlayerAnimationController : MonoBehaviour
    {
        
        Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey("w"))
            {
                animator.SetBool("isWalking",true);
            }
        }
    }
}
