using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Serialization;

public class FPSMovement : MonoBehaviour
{
    Animator animator;

    const float locomotionAnimationSmoothTime = .1f;

    public float walkSpeed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    public Transform target;
    Interactable newTarget;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        target = GetComponent<Transform>();
        target = newTarget.interactionTransform;

    }

    void Update()
    {
        if (controller.velocity != Vector3.zero)
            FaceTarget();

        if (controller.isGrounded)
        { 
            float speedPercent = controller.velocity.magnitude / walkSpeed; //divide by maxspeed (run not used here)
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

    void FaceTarget()
    {
        Vector3 direction = (controller.velocity);//.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}