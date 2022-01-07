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
    private Transform camera;
    private PlayerInputHandle playerInputHandle;
    private Vector3 moveVector;
    public Transform transform;
    
    [Header("movement flags")]
    public bool isSprinting;
    public bool isWalking;
    public bool isGrounded;
    public bool isJumping;


    [Header("values")] 
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float speed = 30f;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float replaceSlopeOffset;

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

    [Header("character controller")] private CharacterController controller;

    [Header("gravity settings")] 
    [SerializeField, Range(-100f, 100f)] private float gravity;
    
    private void Awake()
    {
        camera = Camera.main.transform;
        controller = gameObject.GetComponent<CharacterController>();
        
        timeToApex = jumpTime / 2;
        //gravity = (-2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        initVelocity = (2 * jumpHeight) / timeToApex;
    }

    private void Start()
    {
        playerInputHandle = gameObject.GetComponent<PlayerInputHandle>();
        timer = jumpCoolDown;

    }

    public void HandleAllPlayerMovement()
    {
        HandleCCJumping();   
        HandleGravity();
        if (playerManager.isInteracting)
        {
            Debug.Log("player is interacting");
            return;
        }
        HandleMovement();
        HandleTurns();
    }

    private void HandleMovement()
    {
        if (playerInputHandle.jumpInput)
        {
            return;
        }
        if (isGrounded && !playerInputHandle.jumpInput)
        {
            Debug.Log("HandleMovement is called");
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
                Debug.Log("we can jump");
                //moveVector.y += initVelocity * 1.5f;
                moveVector.y += Mathf.Sqrt(-2f * gravity * jumpHeight);
                controller.Move(moveVector * Time.deltaTime);
                animatorManager.animator.SetBool("isJumping", true);
                animatorManager.PlayTargetAnimation("Jumping", false);
            }  
            if(timer >= 0f)
            {
                Debug.Log("timer going down");
                timer -= Time.deltaTime;
            }
        }
        else
        {
            Debug.Log("timer reset");
            timer = jumpCoolDown;
            if(timer >= 0f)
            {
                timer -= Time.deltaTime;
            }
            isJumping = false; 
        }
    }

    private void HandleGravity()
    {
        if (!isGrounded && !isJumping)
        {
            //Gravity by default is set to a negative float.
            moveVector.y += gravity * Time.deltaTime;
                    controller.Move(moveVector * Time.deltaTime);
        }
        Vector3 castPos = new Vector3(gameObject.transform.position.x,
        gameObject.transform.position.y - checkHeightOffset,
        gameObject.transform.position.z);

        if(Physics.CheckSphere(castPos, debugRadius, layers, QueryTriggerInteraction.Ignore)){
            Debug.Log("Player is grounded");
            //when we touch the ground, we check to play the landing animation first
            if(!isGrounded){
                //animatorManager.PlayTargetAnimation("Land", true);
            }
            isGrounded = true;
            isJumping = false;
        }
        else
        {
            isGrounded = false;
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

    public Vector3 GetPlayerDirection(){
    Vector3 targetDir = Vector3.zero;
        targetDir = camera.forward * playerInputHandle.vertical;
        targetDir = targetDir + camera.right * playerInputHandle.horizontal;
        targetDir.y = 0;
        targetDir.Normalize();

        return targetDir;
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
