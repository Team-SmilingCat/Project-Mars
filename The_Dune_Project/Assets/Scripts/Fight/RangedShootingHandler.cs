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
    
    [Header("range of the ranged attack")] 
    [SerializeField] private float rangedDistance;
    [SerializeField] private LayerMask hittableEntities;

    [SerializeField] public float gunRotationSpeed { get; set; }
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
        currentPlayerRotation = gameObject.transform.rotation;
    }
    public void HandleShootingAttack()
    { 
        if(!isAiming)
        {
            //we cast again since the player might have a changed rotation once they start moving around.
            currentPlayerRotation = gameObject.transform.rotation;
        }
        Vector3 worldTarget = Vector3.zero;
        Vector2 crossHairLoc = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray rayTarget = Camera.main.ScreenPointToRay(crossHairLoc);
        
        //handle raycast
        if (Physics.Raycast(rayTarget, out RaycastHit hit, rangedDistance, hittableEntities))
        {
            rayConfirmer.position = hit.point;
            float distancePlayerObject = Vector3.Distance(rayConfirmer.position, transform.position);
            Debug.Log(distancePlayerObject);
            if (playerInputHandle.rightClickInput && distancePlayerObject > minDistanceAllowedToAim)
            {
                gameObject.GetComponent<Animator>().SetLayerWeight(2,1);
                //uiReticle.SetActive(true);
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
                //this is when the player returns to a non-aiming state
                gameObject.GetComponent<Animator>().SetLayerWeight(2,0);
                isAiming = false;
                //uiReticle.SetActive(false);
                gameObject.transform.rotation = currentPlayerRotation;
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
