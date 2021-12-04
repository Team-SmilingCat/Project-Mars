using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RangedShootingHandler : MonoBehaviour
{
    [SerializeField] private PlayerInputHandle playerInputHandle;
    
    [Header("range of the ranged attack")] [SerializeField]
    private float rangedDistance;
    [SerializeField] private LayerMask hittableEntities;
    [SerializeField] private float gunRotationSpeed; 

    [Header("Debug")] [SerializeField] private Transform rayConfirmer;
    
    
    public void HandleShootingAttack()
    {
        Vector3 worldTarget = Vector3.zero;
        Vector2 crossHairLoc = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray rayTarget = Camera.main.ScreenPointToRay(crossHairLoc);
        
        //handle raycast
        if (Physics.Raycast(rayTarget, out RaycastHit hit, rangedDistance, hittableEntities))
        {
            rayConfirmer.position = hit.point;
        }

        if (playerInputHandle.rightClickInput)
        {
            worldTarget = hit.point;
            //worldTarget.y = gameObject.transform.position.y;
            Vector3 aimDirection = (worldTarget - gameObject.transform.position).normalized;

            var rotation = Quaternion.LookRotation(aimDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * gunRotationSpeed);
            
            if (playerInputHandle.leftClickInput)
            {
                Debug.Log("only shooting");
            }
        }
    }

    void Update()
    {
        
    }
}
