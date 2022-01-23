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

    [SerializeField] private float dashScale;

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
        timer = jumpCoolDown;
        lastYPos = transform.position.y;
        stepOffset = controller.stepOffset;
    }

    public void HandleAllPlayerMovement()
    {
              HandleGravity();  
        HandleFalling(); 
        HandleCCJumping();

        if(!playerHookHandler.finishedHook){
            return;
        }
        if (playerManager.isInteracting){
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
            moveVector = moveVector + camera.right * playerInputHandle.horizontal;
            moveVector.y = 0;
            moveVector.Normalize();
            if(rangedShootingHandler.isAiming){
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

        if(Physics.CheckSphere(castPos, debugRadius, layers, QueryTriggerInteraction.Ignore))
        {
            //Lerp to proper position when going down
            if(isJumping){
                isGrounded = true;
                isJumping = false;
                animatorManager.EnableLanding();
            }
            else{
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

    public void HandleDash(){
        Vector3 targetDir = Vector3.zero;
        targetDir = camera.forward * playerInputHandle.vertical;
        targetDir = targetDir + camera.right * playerInputHandle.horizontal;
        targetDir.y = 0;
        targetDir.Normalize();

        controller.Move(targetDir * dashScale * Time.deltaTime);
    }

    public void HandleFalling()
    {
        if(isGrounded)
        {
            isFalling = false;
            velocityY = 0;
            controller.stepOffset = stepOffset;
        }
        if (!isGrounded && isJumping && !isFalling && velocityY < -30.0f)
        {
            animatorManager.PlayTargetAnimation("FallingJumped", false);
            isFalling = true;
            controller.stepOffset = 0;

        }
        else if (!isGrounded && !isJumping && !isFalling && velocityY < -20.0f)
        {
            animatorManager.PlayTargetAnimation("Falling", false);
            isFalling = true;
            controller.stepOffset = 0;

        }
        

    }

    public void resetVelocityY(){
        velocityY = 0;
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
