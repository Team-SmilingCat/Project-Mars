using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private CameraManager cameraManager;
    private Animator animator;
    private PlayerInputHandle playerInputHandle;
    private PlayerMovement playerMovement;
    private RangedShootingHandler rangedShootingHandler;
    private PlayerAttack playerAttack;

    public bool isInteracting;
    public bool canCombo;
    public bool canRotateDuringAttack;
    
    protected enum PlayerStates
    {
        Active,
        Inventory
    };
    
    protected PlayerStates playerState;
    
    private void Awake()
    {
        playerInputHandle = gameObject.GetComponent<PlayerInputHandle>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        rangedShootingHandler = gameObject.GetComponent<RangedShootingHandler>();
        animator = gameObject.GetComponent<Animator>();
        playerAttack = gameObject.GetComponent<PlayerAttack>();
    }

    private void Start()
    {
        playerState = PlayerStates.Active;
    }

    private void Update()
    {
        switch (playerState)
        {
            case PlayerStates.Active:
                Cursor.lockState = CursorLockMode.Locked;
                playerInputHandle.HandleAllInputs();
                playerMovement.HandleAllPlayerMovement();
                rangedShootingHandler.HandleShootingAttack();
                break;
            case PlayerStates.Inventory:
                playerInputHandle.HandleAllInventoryInputs();
                break;
        }

    }

    private void LateUpdate()
    {
        isInteracting = animator.GetBool("isInteracting");
        canCombo = animator.GetBool("canCombo");
        canRotateDuringAttack = animator.GetBool("canRotateDuringAttack");
        animator.SetBool("isJumping", playerMovement.isJumping);
        animator.SetBool("isGrounded", playerMovement.isGrounded);
        playerInputHandle.leftClickInput = false;
        cameraManager.HandleCameraFunctions();
    }

    //TODO: Observers when refactoring
    public void SwitchToInventoryState()
    {
        playerState = PlayerStates.Inventory;
    }

    public void SwitchToActiveState()
    {
        playerState = PlayerStates.Active;
    }
    
}
