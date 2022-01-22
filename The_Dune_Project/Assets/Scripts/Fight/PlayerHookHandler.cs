using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHookHandler : MonoBehaviour
{
    [SerializeField] GameObject hook;
    [SerializeField] GameObject boneToMove;
    [SerializeField] float distance;
    [SerializeField] float rotationSpeed;
    [SerializeField] LayerMask layers;
    [SerializeField] RangedShootingHandler rangedShootingHandler;
    private CharacterController controller;
    public bool isHooking;
    public bool finishedHook;
    private Vector3 target;
    private Vector3 momentum;
    [SerializeField] float grappleLimit;
    [SerializeField] float launchSpeedMin;
    [SerializeField] float launchSpeedMax;
    [SerializeField] float constant;

    [SerializeField] Vector3 returnPosOffset;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        finishedHook = true;
        isHooking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHooking)
        {
            MoveToGrappledPosition();
        }
    }

    public void UseHook(){
        if (isHooking)
        {
            return;
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));

            if (Physics.Raycast(ray, out RaycastHit hit, distance, layers, QueryTriggerInteraction.Collide))
            {
                isHooking = true;
                target = hit.point;
                Vector3 direction = (target - hook.transform.position).normalized;
                boneToMove.transform.position = Vector3.Slerp(boneToMove.transform.position,
                    hit.point, rotationSpeed * Time.deltaTime);
            }
            else
            {
                isHooking = false;
            }
        }
    }

    private void MoveToGrappledPosition(){
        if (isHooking)
        {
            Vector3 direction = (target - transform.position).normalized;
            controller.Move(direction * constant * Mathf.Clamp(Vector3.Distance(transform.position, target), launchSpeedMin, launchSpeedMax) * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < grappleLimit)
            {
                finishedHook = true;
                isHooking = false;
                ResetHook();
            }   
        }
    }

    public void ResetHook(){
        boneToMove.transform.position = new Vector3(
        boneToMove.transform.parent.transform.position.x + returnPosOffset.x, 
        boneToMove.transform.parent.transform.position.y + returnPosOffset.y, 
        boneToMove.transform.parent.transform.position.z + returnPosOffset.z);
    }

}


