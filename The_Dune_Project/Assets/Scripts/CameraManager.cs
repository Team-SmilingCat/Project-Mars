using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CameraManager : MonoBehaviour
{
    //object that the cam will follow
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private PlayerInputHandle inputHandle;
    private  Transform cameraTransform;
    private Vector3 camVelocity;
    private Vector3 camPos;
    public float speed = 0.5f;
    
    [Header("Camera Rotation Settings")]
    public float lookAt; //up and down
    public float pivotAngle; //left right
    public float rotationSpeed = 2f;
    public float pivotSpeed = 2f;

    public float minPivot = -80;
    public float maxPivot = 80;

    [Header("Camera Collider Settings")] 
    public float defaultPos;
    public float colliderRadius = 1f;
    public float cameraCollisionOffset = 0.5f; //how much camera jump off objects its colliding with
    public float minimumCollisionOffset = 0.5f;
    public float cameraLerpSpeed = 0.2f;
    public LayerMask collisionLayers;
    private Vector3 direction;
    private float hitDistDebug;
    
    
    public void Awake()
    {
        camVelocity = new Vector3();
        cameraTransform = Camera.main.transform;
        defaultPos = cameraTransform.localPosition.z;
    }

    public void HandleCameraFunctions()
    {
        FollowTarget();
        RotateCamera();
        HandleCollisions();
    }

    private void HandleCollisions()
    {
        float targetPos = defaultPos;
        RaycastHit hit;
        direction = (cameraTransform.position - cameraPivot.position).normalized;

        //query trigger interaction ignores all collider triggers in the scene
        if (Physics.SphereCast(cameraPivot.transform.position, colliderRadius, direction, out hit, Mathf.Abs(targetPos), collisionLayers, QueryTriggerInteraction.Ignore))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPos -= distance - cameraCollisionOffset;
            hitDistDebug = hit.distance;
        }

        if (Mathf.Abs(targetPos) < minimumCollisionOffset)
        {
            targetPos = targetPos - minimumCollisionOffset;
        }
        else
        {
            hitDistDebug = colliderRadius;
        }

        camPos.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPos, cameraLerpSpeed * Time.deltaTime);
        cameraTransform.localPosition = camPos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(cameraPivot.transform.position, cameraPivot.transform.position + direction * hitDistDebug);
        Gizmos.DrawWireSphere(cameraPivot.transform.position + direction * hitDistDebug, colliderRadius);
    }

    private void FollowTarget()
    {
        Vector3 targetPos = Vector3.SmoothDamp(transform.position, targetTransform.position, ref camVelocity, speed);
        gameObject.transform.position = targetPos;
    }

    private void RotateCamera()
    {
        lookAt += (inputHandle.mouseX * rotationSpeed);
        pivotAngle -= (inputHandle.mouseY * pivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minPivot, maxPivot);

        //for the up and down rotation movement of the camera
        Vector3 rotation = Vector3.zero;
        rotation.y = lookAt;
        Quaternion targetRot = Quaternion.Euler(rotation);
        transform.rotation = targetRot;
        
        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRot = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRot;
    }
}
