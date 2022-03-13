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
    [SerializeField] private float fallTimer = 0.5f;
    [SerializeField] private float fallCoolDown = 0.15f;

    private float lastYPos;
    public bool isFalling;
    private float stepOffset;
    private float fallTime;
    private float manualVelocity;

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
    [SerializeField] private float slopeRayLength;

    [SerializeField] private float slopeJumpHeight;

    [Header("dash settings")]
    [SerializeField] private float dashScale;
    [SerializeField] private float dashCooldown;
    [SerializeField] private AnimationCurve scaledMovmentCurve;
    private float dashTimer;
    private bool canDash;
    private bool isDashing;
    

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

    private void LateUpdate()
    {
        if (!isGrounded)
        {
            float curYPos = transform.position.y;
            velocityY = (curYPos - lastYPos) / Time.deltaTime;
            lastYPos = curYPos;
        }

    }

    private void InitMovementSettings()
    {
        timer = jumpCoolDown;
        lastYPos = transform.position.y;
        stepOffset = controller.stepOffset;
        isOnSlope = false;
        canDash = true;
        Keyframe lastFrameOfCurve = scaledMovmentCurve[scaledMovmentCurve.length - 1];
        dashTimer = lastFrameOfCurve.time;
    }

    public void HandleAllPlayerMovement()
    {
        HandleGravity();
        CheckForGrounded();
        if (playerManager.isInteracting) return;
        if (isDashing) return;
        HandleCCJumping();
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
            moveVector = AdjustOnUnevenTerrain(moveVector);
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
            animatorManager.ModifyBoolParams("isJumping", false);
            if (timer <= 0f && playerInputHandle.jumpInput)
            {
                animatorManager.ModifyBoolParams("isJumping", true);
                moveVector.y += Mathf.Sqrt(-2f * gravity * jumpHeight);
                controller.Move(moveVector * Time.deltaTime);
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

            playerInputHandle.ResetJumpInput();
        }
        
    }

    private void HandleGravity()
    {
        if (!isGrounded)
        {
            if (!playerInputHandle.jumpInput)
            {
                animatorManager.ModifyBoolParams("isFalling", true);
            }
            moveVector = AdjustOnUnevenTerrain(moveVector);
            moveVector.y += gravity * Time.deltaTime;
            controller.Move(moveVector * Time.deltaTime);
        }
    }

    private void CheckForGrounded()
    {
        Vector3 sphereCaster = new Vector3(transform.position.x,
            transform.position.y - checkHeightOffset, transform.position.z);
        isGrounded = Physics.CheckSphere(sphereCaster, debugRadius, layers, QueryTriggerInteraction.Ignore);
        animatorManager.ModifyBoolParams("canLand", isGrounded);
        animatorManager.ModifyBoolParams("isJumping", !isGrounded);
    }

    private Vector3 AdjustOnUnevenTerrain(Vector3 velocity)
    {
        var ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit,  slopeRayLength, slopeLayer, QueryTriggerInteraction.Collide))
        {
            //hit normal returns to us the normal of the slope that the ray hit.
            var slopeAngle = Quaternion.FromToRotation(Vector3.up, hit.normal);
            var newVelocity = slopeAngle * velocity;

            if(newVelocity.y < 0)
            {
                return newVelocity;
            }    
        }

        return velocity;
    }

    private IEnumerator HandleDashCD()
    {
        isDashing = false;
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public IEnumerator HandleDodge()
    {
        if(moveVector.magnitude == 0 || playerManager.isInteracting)
        {
            yield return null;
        }
        else if (canDash)
        {
            isDashing = true;
            float timer = 0;
            animatorManager.PlayTargetAnimation("dive", true);
            while (timer < dashTimer)
            {
                float speed = scaledMovmentCurve.Evaluate(timer);
                Vector3 dashDirection =
                    (transform.forward * speed) + (Vector3.up * gravity);
                controller.Move(dashDirection);
                timer += Time.deltaTime;
                yield return null;
            }
        }

        yield return StartCoroutine(HandleDashCD());
    }

    public void ResetVelocityY()
    {
        velocityY = 0;
    }

    public void HandleKnockBack(Vector3 dir)
    {
        moveVector += dir * kbForce;
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
