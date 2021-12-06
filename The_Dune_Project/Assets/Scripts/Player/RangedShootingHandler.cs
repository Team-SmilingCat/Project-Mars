using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class RangedShootingHandler : MonoBehaviour
{
    [SerializeField] private PlayerInputHandle playerInputHandle;
    
    [Header("range of the ranged attack")] [SerializeField]
    private float rangedDistance;
    [SerializeField] private LayerMask hittableEntities;
    [SerializeField] private float gunRotationSpeed;
    [SerializeField] private GameObject particle;
    [SerializeField] private bool leftClickisHeldDownWithRight;

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
            if (playerInputHandle.rightClickInput)
            {
                worldTarget = hit.point;
                //worldTarget.y = gameObject.transform.position.y;
                Vector3 aimDirection = (worldTarget - gameObject.transform.position).normalized;

                var rotation = Quaternion.LookRotation(aimDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * gunRotationSpeed);
            
                if(playerInputHandle.leftClickInput)
                {
                    GameObject clone = (GameObject) Instantiate(particle, rayConfirmer.position, Quaternion.identity);
                    Destroy(clone, 1.5f);
                    Debug.Log("only shooting");
                    //checks if the target of the raycast is a attackable emnemy
                    if (hit.collider.gameObject.tag.Equals("Mob"))
                    {
                        hit.collider.gameObject.GetComponent<EntityStatsManager>().TakeDamage(10);
                    }
                }
            }
        }
    }

    public void handleBullets()
    {
        
    }

    void Update()
    {
        
    }
}
