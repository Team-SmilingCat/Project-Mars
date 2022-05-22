using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    /* Player Scripts */
    private PlayerInputHandle playerInputHandle;
    private PlayerMovement playerMovement;
    private RangedShootingHandler rangedShootingHandler;
    private PlayerAttack playerAttack;
    [SerializeField] private PlayerUIManager playerUIManager;

    [SerializeField] private CameraManager cameraManager;
    private Animator animator;

    public bool isInteracting;
    public bool canCombo;
    public bool canRotateDuringAttack;
    
    
    public enum PlayerStates
    {
        Active,
        Inventory,
        Aim,
        Shoot
    };
    
    public PlayerStates playerState; 
    
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
    
    private bool PlayerHasAllRequiredScripts()
    {
        if (
            playerInputHandle != null &&
            playerMovement != null &&
            rangedShootingHandler != null &&
            animator != null &&
            playerAttack != null 
            )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PlayerLoop()
    {
        if (!PlayerHasAllRequiredScripts())
        {
            Debug.Log("missing scripts! check player manager");
            return;
        }

        switch (playerState)
        {
            case PlayerStates.Active:
                //Cursor.lockState = CursorLockMode.Locked;
                playerInputHandle.HandleAllInputs();
                playerUIManager.CheckForInventoryInput();
                playerMovement.HandleAllPlayerMovement();
                break;
            case PlayerStates.Inventory:
                //check for condition
                playerUIManager.CheckForInventoryClosingInput();
                break;
        }
    }

    public void PlayerLateUpdate()
    {
        if (!PlayerHasAllRequiredScripts())
        {
            return;
        }
        isInteracting = animator.GetBool("isInteracting");
        canCombo = animator.GetBool("canCombo");
        canRotateDuringAttack = animator.GetBool("canRotateDuringAttack");
        animator.SetBool("isJumping", playerMovement.isJumping);
        animator.SetBool("isGrounded", playerMovement.isGrounded);
        playerInputHandle.leftClickInput = false;
        cameraManager.HandleCameraFunctions();
    }

    public void SwitchStates(PlayerStates state)
    {
        playerState = state;
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
