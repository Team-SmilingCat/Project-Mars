using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInputHandle : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveValue;
    public float mouseX;
    public float mouseY;

    public bool isSprintEnabled;
    public bool isWalkEnabled;
    public bool jumpInput;
    public bool leftClickInput;

    private PlayerControls playerControls;
    [SerializeField] private AnimatorManager animatorManager;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerInventoryManager playerInventoryManager;
    [SerializeField] private PlayerManager playerManager;
    private Vector2 movementInput;
    private Vector2 cameraInput;

    [Header("action flags")] public bool flagCombo;

    private void Awake()
    {
        
    }

    public void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            //when we hit key
            playerControls.PlayerMovement.Movement.performed += playerControls => movementInput = playerControls.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            //sets sprint bool to true when we press it
            playerControls.PlayerActions.SprintButton.performed += i => isSprintEnabled = true;
            playerControls.PlayerActions.SprintButton.canceled += i => isSprintEnabled = false;
            playerControls.PlayerActions.WalkButton.performed += i => isWalkEnabled = true;
            playerControls.PlayerActions.WalkButton.canceled += i => isWalkEnabled = false;
            playerControls.PlayerActions.JumpButton.performed += i => jumpInput = true;
            playerControls.PlayerActions.JumpButton.canceled += i => jumpInput = false;
            playerControls.PlayerActions.Lclick.performed += i => leftClickInput = true;
        }
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void TickInput(float delta)
    {
        
    }

    public void HandleAllInputs()
    {
        //call all input functions in here
        MoveInput();
        HandleSprint();
        HandleForcedWalk();
        //HandleJumping();
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

    private void HandleSprint()
    {
        if (isSprintEnabled && moveValue > 0.5f)
        {
            playerMovement.isSprinting = true;
        }
        else
        {
            playerMovement.isSprinting = false;
        }
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
        if (jumpInput)
        {
            jumpInput = false;

            
        }   
    }

    private void HandleAttackInput()
    {
        if (leftClickInput && !playerMovement.isJumping)
        {
            if (playerManager.canCombo)
            {
                flagCombo = true;
                playerAttack.handleAttackSequence(playerInventoryManager.weapon);
                //gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                flagCombo = false;
            }
            else
            {
                if (playerManager.isInteracting) return;
                if (playerManager.canCombo) return;
                playerAttack.handleAttack(playerInventoryManager.weapon);   
                //gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            }
        }
        else
        {
            //gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
         
    }


}
