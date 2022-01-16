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

    [SerializeField]float launchSpeed;
    private Vector3 hookPos;

    private CharacterController controller;
    public bool isHooking;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UseHook(){
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));

        if(Physics.Raycast(ray, out RaycastHit hit, distance, layers, QueryTriggerInteraction.Collide)){
            target = hit.point;
            Vector3 direction = (target - hook.transform.position).normalized;
            var rotation = Quaternion.LookRotation(direction);
            hook.transform.rotation = Quaternion.Slerp(hook.transform.localRotation, 
            rotation, Time.deltaTime * rotationSpeed);
            boneToMove.transform.position = Vector3.Slerp(boneToMove.transform.position, 
            hit.point, rotationSpeed * Time.deltaTime);
            MoveToGrappledPosition();
        }
    }

    private void MoveToGrappledPosition(){
        Vector3 direction = (target - transform.position).normalized;
        controller.Move(direction * launchSpeed * Time.deltaTime);


    }
}


