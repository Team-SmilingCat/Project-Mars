using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerInputHandle playerInputHandle;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private RangedShootingHandler rangedShootingHandler;

    public bool isInteracting;
    public bool canCombo;
    private void Start()
    {
        playerInputHandle = gameObject.GetComponent<PlayerInputHandle>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerInputHandle.HandleAllInputs();
        rangedShootingHandler.HandleShootingAttack();
        
    }

    private void LateUpdate()
    {
        isInteracting = animator.GetBool("isInteracting");
        canCombo = animator.GetBool("canCombo");
        playerMovement.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerMovement.isGrounded);
        playerInputHandle.leftClickInput = false;
        cameraManager.HandleCameraFunctions();
    }

    private void FixedUpdate()
    {
        //movement should be fixed update -> unity specific rule
        playerMovement.HandleAllPlayerMovement();

    }
}
