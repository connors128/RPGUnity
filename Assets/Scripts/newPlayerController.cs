﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class newPlayerController : MonoBehaviour
{
    const float locomotionAnimationSmoothTime = .1f;

    Animator animator;

    CharacterController characterController;

    public float speed = 5f;
    public float rotationSpeed = 240f;
    float gravity = 20f;
    Vector3 moveDir = Vector3.zero;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (v < 0)
            v = 0;

        transform.Rotate(0f, h * rotationSpeed * Time.deltaTime, 0f);

        if(characterController.isGrounded)
        {
            float speedPercent = characterController.velocity.magnitude / speed;
            animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
            moveDir = Vector3.forward * v;

            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;
        }
        moveDir.y -= gravity * Time.deltaTime;

        characterController.Move(moveDir * Time.deltaTime);
    }
}
