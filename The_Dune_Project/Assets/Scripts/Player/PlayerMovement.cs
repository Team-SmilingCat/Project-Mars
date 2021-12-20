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
    Rigidbody rigidbody;

    public Transform transform;
    public GameObject normalCamera;

    [Header("falling settings")] 
    public float airTime;
    public float jumpVelocity;
    public float fallSpeed;
    public LayerMask layers;
    public float rayCastHeightOffset;
    public float debugRadius;
    public float maxDist;
    
    [Header("movement flags")]
    public bool isSprinting;
    public bool isWalking;
    public bool isGrounded;
    public bool isJumping;


    [Header("values")] 
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float speed = 30f;
    [SerializeField] private float sprint = 45f;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float replaceSlopeOffset;

    [Header("jump settings")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float initVelocity;
    [SerializeField] private float jumpTime;
    [SerializeField] private float timeToApex;

    [Header("Handle Slopes")]
    private Vector3 slopeDirection;
    private Vector3 rayCastVector;
    private RaycastHit slopeHit;


    [Header("character controller")] private CharacterController controller;

    [Header("gravity settings")] 
    [SerializeField] private float groundGravity;
    [SerializeField] private float gravity;



    [SerializeField] private float slopeForce;

    [SerializeField] private float slopeForceRayLength;
    
    private void Awake()
    {
        camera = Camera.main.transform;
        controller = gameObject.GetComponent<CharacterController>();
        
        timeToApex = jumpTime / 2;
        gravity = (-2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        initVelocity = (2 * jumpHeight) / timeToApex;
    }

    private void Start()
    {
        playerInputHandle = gameObject.GetComponent<PlayerInputHandle>();

    }

    public void HandleAllPlayerMovement()
    {
        
        //HandleFalls();
        if (playerManager.isInteracting)
        {
            return;
        }
        HandleMovement();
        HandleTurns();
        controller.Move(moveVector * Time.deltaTime);
        
        HandleGravity();
       HandleCCJumping();

    }

    private void HandleMovement()
    {
        if (playerInputHandle.jumpInput)
        {
            return;
        }
        if (controller.isGrounded && !playerInputHandle.jumpInput)
        {
            moveVector = camera.forward * playerInputHandle.vertical;
            moveVector = moveVector + camera.right * playerInputHandle.horizontal;
            moveVector.y = 0;
            moveVector.Normalize();
            if (isSprinting)
            {
                moveVector *= sprint;
            }
            else if(rangedShootingHandler.isAiming){
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
            
        }
    }

    private void HandleTurns()
    {
        //we cannot move during jump
        if (playerInputHandle.jumpInput)
        {
            return;
        }

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
    

    public void HandleCCJumping()
    {
        if (controller.isGrounded && !isJumping && playerInputHandle.jumpInput)
        {
            isJumping = true;
            Debug.Log("jumpinh");
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jumping", false);
            moveVector.y += initVelocity * 1.5f;
        } else if (!playerInputHandle.jumpInput && isJumping && controller.isGrounded)
        {
            isJumping = false;
        }   
    }

    public void HandleGravity()
    {
        if (!controller.isGrounded)
        {
            Debug.Log("applying gravity");
            moveVector.y += gravity * Time.deltaTime;
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(rayCastVector, debugRadius);
    }

    private void FixedUpdate()
    {
        Debug.Log(controller.isGrounded);
        HandleAllPlayerMovement();
    }
}
