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
    [SerializeField]
    private float camPitch;
    [SerializeField] private float camYaw;
    [SerializeField] private float camSpeed;

    [SerializeField] private float maxAngle;
    [SerializeField] private float minAngle;
    [SerializeField] private float angleOffset;

    [Header("Cinemachine")] [SerializeField]
    private GameObject cinemachineTarget;
    
    
    
    public void Awake()
    {
        camVelocity = new Vector3();
        cameraTransform = Camera.main.transform;
    }

    public void HandleCameraFunctions()
    {
        //FollowTarget();
        RotateCamera();
        //HandleCollisions();
    }
    
    private void RotateCamera()
    {
        // if there is an input and camera position is not fixed
        if (inputHandle.getMouseMagnitudeS() >= 0.01f)
        {
            camYaw += inputHandle.mouseX * Time.deltaTime * camSpeed;
            camPitch -= inputHandle.mouseY * Time.deltaTime * camSpeed;
        }

        // clamp our rotations so our values are limited 360 degrees
        camYaw = ClampAngle(camYaw, float.MinValue, float.MaxValue);
        camPitch = ClampAngle(camPitch, this.minAngle, this.maxAngle);

        // Cinemachine will follow this target
        cinemachineTarget.transform.rotation = Quaternion.Euler(camPitch + angleOffset, camYaw, 0.0f);
    }
    
    private float ClampAngle(float angle, float minAngle, float maxAngle)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, minAngle, maxAngle);
    }

}
