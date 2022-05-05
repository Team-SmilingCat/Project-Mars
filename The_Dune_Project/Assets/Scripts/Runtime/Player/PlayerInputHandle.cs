using System;
using System.Collections;
using System.Collections.Generic;
using Scriptable_Objects;
using UnityEngine;

public class PlayerInputHandle : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveValue;
    public float mouseX;
    public float mouseY;

    [Header("action flags")]
    public bool isWalkEnabled;
    public bool jumpInput;
    public bool leftClickInput;
    public bool rightClickInput;
    public bool dashInput;
    public bool hookInput;
    
    [Header("menu flags")]
    public bool inventoryInput;

    public bool shieldInput;

    private PlayerControls playerControls;

    [Header("Player Components")]
    [SerializeField] private AnimatorManager animatorManager;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerInventoryManager playerInventoryManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private RangedShootingHandler rangedShootingHandler;
    private Vector2 movementInput;
    private Vector2 cameraInput;

    [Header("action flags")] 
    public bool flagCombo;

    private void Start()
    {
        inventoryInput = false;
    }

    public void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            //when we hit key
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Movement.canceled += i => movementInput = new Vector2(0,0);
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            //sets sprint bool to true when we press it
            playerControls.PlayerActions.WalkButton.performed += i => isWalkEnabled = true;
            playerControls.PlayerActions.WalkButton.canceled += i => isWalkEnabled = false;
            playerControls.PlayerActions.JumpButton.performed += i => jumpInput = true;
            playerControls.PlayerActions.JumpButton.canceled += i => jumpInput = false;
            playerControls.PlayerActions.Lclick.performed += i => leftClickInput = true;
            playerControls.PlayerActions.Lclick.canceled += i => leftClickInput = false;
            playerControls.PlayerActions.Rclick.performed += i => rightClickInput = true;
            playerControls.PlayerActions.Rclick.canceled += i => rightClickInput = false;
            playerControls.PlayerActions.DashButton.performed += i => dashInput = true;
            playerControls.PlayerActions.DashButton.canceled += i => dashInput = false;
            playerControls.PlayerActions.Hook.performed += i => hookInput = true;
            playerControls.PlayerActions.Hook.canceled += i => hookInput = false;
            playerControls.PlayerActions.ShieldButton.performed += i => shieldInput = true;
            playerControls.PlayerActions.ShieldButton.canceled += i => shieldInput = false;
            
            //inventory inputs
            playerControls.MenuControls.InventoryInstance.performed += i => inventoryInput = true;
            playerControls.MenuControls.InventoryInstance.canceled += i => inventoryInput = false;
        }
        playerControls.Enable();
    }

    private void OnDisable()
    {
        if(playerControls != null)
        {
            return;
        }
        playerControls.Disable();
    }

    public float getMouseMagnitudeS()
    {
        return cameraInput.sqrMagnitude;
    }

    public void HandleAllInputs()
    {
        //call all input functions in here
        HandleJumping();
        MoveInput();
        HandleForcedWalk();
        HandleDashInput();
        HandleAimingInput();
        HandleAttackInput();
    }

    private void MoveInput()
    {
        //initialize inputs
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveValue = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        animatorManager.UpdateAnimatorValues(0, moveValue, playerMovement.isSprinting, playerMovement.isWalking);
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }


    private void HandleForcedWalk()
    {
        if (isWalkEnabled && moveValue > 0.5f)
        {
            playerMovement.isWalking = true;
        }
        else
        {
            playerMovement.isWalking = false;
        }
    }

    private void HandleJumping()
    {


    }

    private void HandleAttackInput()
    {
        if (playerInventoryManager.weapon.type.Equals("melee"))
        {
            if (leftClickInput && !rightClickInput && playerMovement.isGrounded)
            {
                if (playerManager.canCombo)
                {
                    flagCombo = true;
                    playerAttack.handleMeleeAttackSequence((MeleeWeapon)playerInventoryManager.weapon);
                    //gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    flagCombo = false;
                }
                else
                {
                    if (playerManager.isInteracting) return;
                    if (playerManager.canCombo) return;
                    playerAttack.handleMeleeAttack((MeleeWeapon)playerInventoryManager.weapon);
                    //gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                }
            }

            if (rightClickInput && !leftClickInput && playerMovement.isGrounded)
            {
                if (playerManager.isInteracting) return;
                playerAttack.HandleHeavyMeleeAttack((MeleeWeapon)playerInventoryManager.weapon);
            }
        }

    }

    private void HandleAimingInput()
    {
        if(!playerMovement.isJumping)
        {
            rangedShootingHandler.HandleShootingAttack();
        }
    }


    private void HandleDashInput()
    {
        if (dashInput)
        {
            dashInput = false;
            StartCoroutine(playerMovement.HandleDodge());
        }
    }

    private void HandleShieldInput()
    {
        //TODO
    }

    public void ResetJumpInput()
    {
        jumpInput = false;
    }

    public void HandleInventoryInput()
    {
        if (inventoryInput)
        {
            inventoryInput = false;
        }
    }



}
