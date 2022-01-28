using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Fight;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class RangedShootingHandler : MonoBehaviour
{
    [Header("spine to rotate on playermodel")]
    [SerializeField] GameObject spineToRotate;

    [Header("Reticle UI object")]
    [SerializeField] Sprite aimningCrossHair;
    [SerializeField] Sprite canHitCrossHair;

    [Header("character manager scripts accessor")]
    [SerializeField] private PlayerInputHandle playerInputHandle;
    [SerializeField] private AnimatorManager animatorManager;
    [SerializeField] private PlayerUIManager playerUIManager;
    
    [Header("range of the ranged attack")] 
    [SerializeField] private float rangedDistance;
    [SerializeField] private LayerMask hittableEntities;

    [SerializeField] private float gunRotationSpeed;
    [SerializeField] private GameObject particle;
    [SerializeField] private bool leftClickisHeldDownWithRight;
    private Quaternion currentPlayerRotation;

    [Header("Debug")] 
    [SerializeField] private Transform rayConfirmer;

    [Header("settings for movement with aiming")] 
    [SerializeField] public bool isAiming;
    [SerializeField] public Quaternion aimVector { get; set; }

    [SerializeField] private float minDistanceAllowedToAim;

    void Start(){
        currentPlayerRotation = spineToRotate.transform.rotation;
    }
    public void HandleShootingAttack()
    { 
        if(!isAiming)
        {
            //we cast again since the player might have a changed rotation once they start moving around.
            currentPlayerRotation = spineToRotate.transform.rotation;
        }
        Vector3 worldTarget = Vector3.zero;
        Vector2 crossHairLoc = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray rayTarget = Camera.main.ScreenPointToRay(crossHairLoc);
        
        //handle raycast
        if (Physics.Raycast(rayTarget, out RaycastHit hit, rangedDistance, hittableEntities))
        {
            rayConfirmer.position = hit.point;
            float distancePlayerObject = Vector3.Distance(rayConfirmer.position, transform.position);
            if (playerInputHandle.rightClickInput && distancePlayerObject > minDistanceAllowedToAim)
            {
                gameObject.GetComponent<Animator>().SetLayerWeight(2,1);
                playerUIManager.ChangeCrossHair(aimningCrossHair);
                worldTarget = hit.point;
                Vector3 aimDirection = (worldTarget - spineToRotate.transform.position).normalized;

                var rotation = Quaternion.LookRotation(aimDirection);
                aimVector = rotation;
                spineToRotate.transform.rotation = Quaternion.Slerp(spineToRotate.transform.rotation, rotation, Time.deltaTime * gunRotationSpeed);
                isAiming = true;
                if(playerInputHandle.leftClickInput)
                {
                    animatorManager.PlayTargetAnimation("shoot", true);
                    GameObject clone = (GameObject) Instantiate(particle, rayConfirmer.position, Quaternion.identity);
                    Destroy(clone, 1.5f);
                    if (hit.collider.gameObject.tag.Equals("Mob"))
                    {
                        hit.collider.gameObject.GetComponent<Fighter>().TakeDamage(10);
                    }
                }
            }
            else
            {
                //this is when the player returns to a non-aiming state
                gameObject.GetComponent<Animator>().SetLayerWeight(2,0);
                isAiming = false;
                playerUIManager.DisableCrossHair();
                spineToRotate.transform.rotation = currentPlayerRotation;
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
