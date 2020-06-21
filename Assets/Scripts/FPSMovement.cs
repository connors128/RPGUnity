﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Serialization;

public class FPSMovement : MonoBehaviour
{
    private Animator _animator;

    private const float LocomotionAnimationSmoothTime = .1f;

    public float walkSpeed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    private Vector3 _moveDirection;
    private CharacterController _controller;
    public Transform target;
    private readonly Interactable _newTarget;

    private Camera _camera;
    // public Interactable focus;
    
    public FPSMovement(Interactable newTarget)
    {
        this._newTarget = newTarget;
    }

    void Start()
    {
        _camera = Camera.main;
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        target = GetComponent<Transform>();
    }

    void Update()
    {
        if (_controller.velocity != Vector3.zero)
            FaceTarget();

        if (_controller.isGrounded)
        { 
            float speedPercent = _controller.velocity.magnitude / walkSpeed; //divide by maxspeed (run not used here)
            _animator.SetFloat("speedPercent", speedPercent, LocomotionAnimationSmoothTime, Time.deltaTime);

            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (_camera != null) 
                _moveDirection = _camera.transform.TransformDirection(_moveDirection);
            
            _moveDirection.y = 0.0f; //important for not 'jumping' when looking up
            _moveDirection *= walkSpeed;
            _controller.Move(_moveDirection * Time.deltaTime);

            if (Input.GetButton("Jump"))
                _moveDirection.y = jumpSpeed;
        }
        else
        {
            _controller.Move(_moveDirection * Time.deltaTime);
            _moveDirection.y -= gravity * Time.deltaTime;
        }

        Interactable interactable = _controller.GetComponent<Interactable>();
        if (interactable != null)
        {
            Debug.Log("Interacting with " + interactable.name);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (_controller.velocity.normalized);//.normalized;
        if (Math.Abs(direction.magnitude) > 0f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        }
    }
    
}