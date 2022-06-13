using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingAction : PlayerActions
{
    [SerializeField] private float climbingSpeed;

    private CharacterController playerController;
    private PlayerInputHandle playerInputHandle;
    private Vector3 moveVector;
    private void Start()
    {
        playerController = GetComponent<CharacterController>();
        playerInputHandle = GetComponent<PlayerInputHandle>();
    }

    private void Awake()
    {
        
    }

    private void ClimbUp()
    {
        moveVector = transform.up * playerInputHandle.vertical;
        moveVector.x = 0;
        moveVector.z = 0;
        if (playerInputHandle.vertical >= 0.5f)
        {
            moveVector *= climbingSpeed;
        }

        playerController.Move(moveVector * Time.deltaTime);
    }
    
    public override void Action()
    {
        ClimbUp();
    }
    
    
}
