using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class testMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    private bool isJumping;
    
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    private void HandleGravity()
    {
        if (!controller.isGrounded)
        {
            moveVector.y += -9.8f * Time.deltaTime;
        }
    }

    private void HandleJump()
    {
        if (controller.isGrounded && !isJumping)
        {
            isJumping = true;
            moveVector.y = 40;
        }
    }

    private void handleAll()
    {
        controller.Move(moveVector * Time.deltaTime);
        
        HandleGravity();
        HandleJump();
    }

    private void FixedUpdate()
    {
        handleAll();
    }
}