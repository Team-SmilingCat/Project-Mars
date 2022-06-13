using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR;

public class MoveObjectAction : PlayerActions
{
    [Header("property values")]
    [SerializeField] protected float pushSpeed;
    private Vector3 moveVector;
    private CharacterController playerController;
    
    [Header("Move with Object")] [SerializeField]
    private Transform rayStartPosition;
    [SerializeField] private LayerMask pushableLayer;
    [SerializeField] private float maxRangeDetect;
    [SerializeField] [CanBeNull] private Rigidbody pushedTarget;
    [SerializeField] private float pushMagnitude;

    private PlayerInputHandle playerInputHandle;
    private PlayerManager playerManager;
    private PlayerMovement playerMovement;
    private void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        playerInputHandle = GetComponent<PlayerInputHandle>();
        playerManager = GetComponent<PlayerManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void MoveObject(Rigidbody targetRb, Transform player, float magnitude)
    {
        Vector3 directionOfForce = targetRb.gameObject.transform.position - player.position;
        directionOfForce.y = 0;
        directionOfForce.Normalize();
        
        targetRb.AddForceAtPosition(directionOfForce * magnitude, player.position,
            ForceMode.Impulse);
    }
    
    private void HandlePushMovement()
    {
        moveVector = transform.forward * playerInputHandle.vertical;
        moveVector.y = 0;
        if (playerInputHandle.vertical < 0 || playerInputHandle.horizontal != 0)
        {
            playerManager.SwitchStates(PlayerManager.PlayerStates.Active);
        }
        if (playerInputHandle.vertical >= 0.5f)
        {
            moveVector *= pushSpeed;
        }

        playerController.Move(moveVector * Time.deltaTime);
    }


    public override void Action()
    {
        HandlePushMovement();
        MoveObject(playerMovement.GetPushTarget(), transform, pushSpeed);
    }
}
