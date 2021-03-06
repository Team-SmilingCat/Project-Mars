using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Cinemachine;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CameraManager : MonoBehaviour
{
    //object that the cam will follow
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private PlayerInputHandle inputHandle;

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

    [SerializeField] private float aimSensitivity;
    [SerializeField] private float originalCamSpeed;

    [Header("Camera Aim")] [SerializeField]
    private CinemachineVirtualCamera aimCamera;



    public void Awake()
    {
        originalCamSpeed = camSpeed;
    }

    public void HandleCameraFunctions()
    {
        HandleCameraAim();
        RotateCamera();
    }
    
    private void RotateCamera()
    {
        if (inputHandle.getMouseMagnitudeS() >= 0.01f)
        {
            camYaw += inputHandle.mouseX * Time.deltaTime * camSpeed;
            camPitch -= inputHandle.mouseY * Time.deltaTime * camSpeed;
        }
        
        camYaw = ClampAngle(camYaw, float.MinValue, float.MaxValue);
        camPitch = ClampAngle(camPitch, this.minAngle, this.maxAngle);
        
        cinemachineTarget.transform.rotation = Quaternion.Euler(camPitch + angleOffset, camYaw, 0.0f);
    }

    private void HandleCameraAim()
    {
        if (inputHandle.rightClickInput)
        {
            Debug.Log("right clicking is " + inputHandle.rightClickInput);
            aimCamera.gameObject.SetActive(true);
            camSpeed = aimSensitivity;
        }
        else
        {
            aimCamera.gameObject.SetActive(false);
            camSpeed = originalCamSpeed;
        }
        
    }

    private void setSensitivity(float d)
    {
        camSpeed = d;
    }
    
    private float ClampAngle(float angle, float minAngle, float maxAngle)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, minAngle, maxAngle);
    }

}
