using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FPSMovement : MonoBehaviour
{
    Animator animator;

    const float locomotionAnimationSmoothTime = .1f;

    public float walkSpeed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float runSpeed = 8.0F;
    public float gravity = 20.0F;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {


        if(controller.isGrounded)
        {
            if(controller.velocity != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(controller.velocity);

            float speedPercent = controller.velocity.magnitude / walkSpeed;
            animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
            moveDirection.y = 0.0f; //important for not 'jumping' when looking up
            moveDirection *= walkSpeed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}