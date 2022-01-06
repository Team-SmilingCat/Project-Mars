using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Fight;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class RangedShootingHandler : MonoBehaviour
{
    [Header("Reticle UI object")]
    [SerializeField] GameObject uiReticle;

    [Header("character manager scripts accessor")]
    [SerializeField] private PlayerInputHandle playerInputHandle;
    [SerializeField] private AnimatorManager animatorManager;
    
    [Header("range of the ranged attack")] [SerializeField]
    private float rangedDistance;
    [SerializeField] private LayerMask hittableEntities;

    [SerializeField] public float gunRotationSpeed { get; set; }
    [SerializeField] private GameObject particle;
    [SerializeField] private bool leftClickisHeldDownWithRight;

    [Header("Debug")] [SerializeField] private Transform rayConfirmer;

    [Header("settings for movement with aiming")] [SerializeField]
    public bool isAiming;
    [SerializeField] public Quaternion aimVector { get; set; }

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
                gameObject.GetComponent<Animator>().SetLayerWeight(2,1);
                uiReticle.SetActive(true);
                worldTarget = hit.point;
                //worldTarget.y = gameObject.transform.position.y;
                Vector3 aimDirection = (worldTarget - gameObject.transform.position).normalized;

                var rotation = Quaternion.LookRotation(aimDirection);
                aimVector = rotation;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * gunRotationSpeed);
                isAiming = true;
                if(playerInputHandle.leftClickInput)
                {
                    Debug.Log("only shooting");
                    animatorManager.PlayTargetAnimation("shoot", true);
                    GameObject clone = (GameObject) Instantiate(particle, rayConfirmer.position, Quaternion.identity);
                    Destroy(clone, 1.5f);
                    //checks if the target of the raycast is a attackable emnemy
                    if (hit.collider.gameObject.tag.Equals("Mob"))
                    {
                        hit.collider.gameObject.GetComponent<Fighter>().TakeDamage(10);
                    }
                }
            }
            else
            {
                gameObject.GetComponent<Animator>().SetLayerWeight(2,0);
                isAiming = false;
                uiReticle.SetActive(false);
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
