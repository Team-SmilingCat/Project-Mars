using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private AnimatorManager animatorManager;
    [SerializeField] private RangedShootingHandler rangedShootingHandler;
    [SerializeField] private PlayerHookHandler playerHookHandler;
    private Transform camera;
    private PlayerInputHandle playerInputHandle;
    private Vector3 moveVector;
    public Transform transform;
    
    [Header("movement flags")]
    public bool isSprinting;
    public bool isWalking;
    public bool isGrounded;
    public bool isJumping;
    public bool isGrappled;


    [Header("movement values")] 
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float speed = 30f;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float acceleration;

    [Header("jump settings")]
    [SerializeField, Range(0f, 100f)] private float jumpHeight;
    [SerializeField] private float initVelocity;
    [SerializeField] private float jumpTime;
    [SerializeField] private float timeToApex; 
    [SerializeField] float jumpCoolDown;
    [SerializeField] float timer;


    [Header("falling settings")] 
    [SerializeField] LayerMask layers;
    [SerializeField] float checkHeightOffset;
    [SerializeField] float debugRadius;

    private float lastYPos;
    public bool isFalling;
    private float stepOffset;
    private float fallTime;

    [Header("character controller")] 
    private CharacterController controller;

    [Header("gravity settings")] 
    [SerializeField, Range(-100f, 100f)] private float gravity;

    [SerializeField, Range(-100f, 100f)] private float playerToGroundRayOffset;

    [SerializeField] float groundHeightLimit;
    private float velocityY;
    
    [Header("slope settings")]
    [SerializeField] private float slopeAngleLimit;
    [SerializeField] private bool isOnSlope;
    [SerializeField] LayerMask slopeLayer;

    [SerializeField] private float slopeJumpHeight;

    [Header("dash settings")]
    [SerializeField] private float dashScale;
    [SerializeField] private float dashCooldown;
    private bool canDash;

    [Header("knockback settings")] 
    [SerializeField] private float kbForce;


    private void Awake()
    {
        camera = Camera.main.transform;
        controller = gameObject.GetComponent<CharacterController>();
        timeToApex = jumpTime / 2;
        initVelocity = (2 * jumpHeight) / timeToApex;
    }

    private void Start()
    {
        playerInputHandle = gameObject.GetComponent<PlayerInputHandle>();
        InitMovementSettings();
    }

    private void InitMovementSettings()
    {
        timer = jumpCoolDown;
        lastYPos = transform.position.y;
        stepOffset = controller.stepOffset;
        isOnSlope = false;
        canDash = true;
    }

    public void HandleAllPlayerMovement()
    {
        HandleGravity();  
        HandleFalling(); 
        if (playerManager.isInteracting){
            return;
        }
        HandleCCJumping();
        if(!playerHookHandler.finishedHook){
            return;
        }
        HandleMovement();
        HandleTurns();
    }

    private void HandleMovement()
    {

        if (isGrounded && !playerInputHandle.jumpInput)
        {
            moveVector = camera.forward * playerInputHandle.vertical;
            moveVector += camera.right * playerInputHandle.horizontal;
            moveVector.y = 0;
            moveVector.Normalize();
            if(rangedShootingHandler.isAiming)
            {
                moveVector *= walkSpeed;
            }
            else if (isWalking)
            {
                moveVector *= walkSpeed;
            }
            else
            {
                if (playerInputHandle.moveValue >= 0.5f)
                {
                    moveVector *= speed;
                }
                else
                {
                    moveVector *= walkSpeed;

                }
            }
            controller.Move(moveVector * Time.deltaTime);
        }
    }

    private void HandleTurns()
    {
        if (rangedShootingHandler.isAiming)
        {
            transform.rotation = rangedShootingHandler.aimVector;
            return;
        }
        
        Vector3 targetDir = Vector3.zero;
        targetDir = camera.forward * playerInputHandle.vertical;
        targetDir = targetDir + camera.right * playerInputHandle.horizontal;
        targetDir.y = 0;
        targetDir.Normalize();

        if (targetDir == Vector3.zero) targetDir = transform.forward;

        //looking towards the target direction
        Quaternion targetRot = Quaternion.LookRotation(targetDir);
        //lerps to the rotation target
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);

        transform.rotation = playerRotation;

    }
    

    private void HandleCCJumping()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -Vector3.up, out hit, groundHeightLimit, slopeLayer))
        {
            if(Vector3.Angle(Vector3.up, hit.normal) < slopeAngleLimit)
            {
                isOnSlope = true;
            }
            else
            {
                isOnSlope = false;
            }
        }
        else
        {
            isOnSlope = false;
        }

        if(isGrounded)
        {
            if (timer <= 0f && playerInputHandle.jumpInput)
            {
                //moveVector.y += initVelocity * 1.5f;
                moveVector.y += Mathf.Sqrt(-2f * gravity * jumpHeight);
                controller.Move(moveVector * Time.deltaTime);
                animatorManager.PlayTargetAnimation("Jumping", false);
                isJumping = true;
            }
            if(timer >= 0f)
            {
                timer -= Time.deltaTime;
            }
        }
        else if((isGrounded && isOnSlope) || isOnSlope)
        {
            if (timer <= 0f && playerInputHandle.jumpInput)
            {
                moveVector.y += Mathf.Sqrt(-2f * gravity * slopeJumpHeight);
                controller.Move(moveVector * Time.deltaTime);
                animatorManager.PlayTargetAnimation("Jumping", false);
                isJumping = true;
            }
            if(timer >= 0f)
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            timer = jumpCoolDown;
            if(timer >= 0f)
            {
                timer -= Time.deltaTime;
            }
        }
    }

    private void LateUpdate()
    {
        if(!isGrounded)
        {
            float curYPos = transform.position.y;
            velocityY  = (curYPos - lastYPos) / Time.deltaTime;
            lastYPos = curYPos;
        }

    }

    private void HandleGravity()
    {
        Vector3 castPos = new Vector3(gameObject.transform.position.x,
        gameObject.transform.position.y - checkHeightOffset,
        gameObject.transform.position.z);

        RaycastHit hit;

        if(Physics.Raycast(transform.position, -Vector3.up, out hit,checkHeightOffset, layers))
        {
            if(Vector3.Distance(transform.position, hit.point) < checkHeightOffset){
                transform.position = Vector3.Lerp(transform.position,
                transform.position + Vector3.up * checkHeightOffset,
                5 * Time.deltaTime
                );
            }
            //Lerp to proper position when going down
            if(isJumping)
            {
                isGrounded = true;
                isJumping = false;
                animatorManager.EnableLanding();
            }
            else
            {
                isGrounded = true;
                isFalling = false;
                animatorManager.EnableLanding();
            }
        }
        else
        {
            isGrounded = false;
            moveVector.y += gravity * Time.deltaTime;
            controller.Move(moveVector * Time.deltaTime);
        }
        
    }

    public void HandleDash()
    {
        if(playerManager.isInteracting)
        {
            return;
        }
        if(isJumping)
        {
            return;
        }
        if(playerInputHandle.moveValue > 0 && canDash)
        {
            moveVector = camera.forward * playerInputHandle.vertical;
            moveVector += camera.right * playerInputHandle.horizontal;
            moveVector.Normalize();
            animatorManager.PlayTargetAnimation("dive", true);
            moveVector.y = 0;
            controller.Move(moveVector * Time.deltaTime * dashScale);
        }
        StartCoroutine(HandleDashCD());

    }

    private IEnumerator HandleDashCD()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void HandleFalling()
    {
        switch (isGrounded)
        {
            case true:
                isFalling = false;
                velocityY = 0;
                controller.stepOffset = stepOffset;
                break;
            case false when isJumping && !isFalling && velocityY < -30.0f:
                animatorManager.PlayTargetAnimation("FallingJumped", false);
                isFalling = true;
                controller.stepOffset = 0;
                break;
            case false when !isJumping && !isFalling && velocityY < -20.0f:
                animatorManager.PlayTargetAnimation("Falling", false);
                isFalling = true;
                controller.stepOffset = 0;
                break;
        }
    }

    public void ResetVelocityY()
    {
        velocityY = 0;
    }

    public void HandleKnockBack(Vector3 dir)
    {
        
    }

    void OnDrawGizmosSelected()
    {
        Vector3 debugVec3 = new Vector3(gameObject.transform.position.x,
        gameObject.transform.position.y - checkHeightOffset,
        gameObject.transform.position.z);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(debugVec3, debugRadius);
    }
}
