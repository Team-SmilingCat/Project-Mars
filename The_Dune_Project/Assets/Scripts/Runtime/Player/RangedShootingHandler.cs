using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.Serialization;
using UnityEngine;
using Fight;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class RangedShootingHandler : MonoBehaviour
{
    [Header("spine to rotate on playermodel")]
    [SerializeField] GameObject spineToRotate;

    [Header("Reticle UI object")]
    [SerializeField] Image aimningCrossHair;

    [Header("character manager scripts accessor")]
    [SerializeField] private PlayerInputHandle playerInputHandle;
    [SerializeField] private AnimatorManager animatorManager;
    private PlayerInventoryManager playerInventoryManager;
    
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

    [Header("Shooting props")]
    [SerializeField] PlayerUIManager playerUIManager;
    private float shotCoolDown;
    [SerializeField] private float reloadCD;
    [SerializeField] private int bulletCount;
    [SerializeField] private int capacity;
    [SerializeField] private bool canShoot;
    
    /* Event notifier for obstacles */
    public static event Action<RangedShootingHandler, int> OnHitEvent;

    private void Awake()
    {
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
    }

    void Start(){
        currentPlayerRotation = spineToRotate.transform.rotation;
        RangedWeapon typedWeapon;
        if (playerInventoryManager.weapon.type.Equals("ranged"))
        {
            typedWeapon = (RangedWeapon)playerInventoryManager.weapon;
            shotCoolDown = typedWeapon.coolDownTime;
        }
        canShoot = true;
    }

    public void AddAmmunition(int n) {
        capacity += n;
    }

    public void SetAmmunition(int n) {
        capacity = n;
    }
    


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
                animatorManager.ModifyBoolParams("isAiming", true);
                playerUIManager.InitAimingUIContent(true);

                worldTarget = hit.point;
                Vector3 aimDirection = (worldTarget - gameObject.transform.position).normalized;
                var rotation = Quaternion.LookRotation(aimDirection);
                aimVector = rotation;
                //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * gunRotationSpeed);
                isAiming = true;    
                ShootRifle(hit);
                return;
            }
            else
            {
                animatorManager.ModifyBoolParams("isAiming", false);
                isAiming = false;
                playerUIManager.InitAimingUIContent(false);
                return;
            }
        }
        animatorManager.ModifyBoolParams("isAiming", false);
    }

    private void ShootRifle(RaycastHit target)
    {
        if (playerInputHandle.leftClickInput && canShoot)
        {
            animatorManager.PlayTargetAnimation("shoot", true);
            GameObject clone = (GameObject)Instantiate(particle, rayConfirmer.position, Quaternion.identity);
            Destroy(clone, 1.5f);
            //checks if the target of the raycast is a attackable emnemy
            if (target.collider.gameObject.tag.Equals("Mob"))
            {
                target.collider.gameObject.GetComponent<Fighter>().TakeDamage(10);
            }
            else
            {
                if (OnHitEvent != null) OnHitEvent(this, target.transform.gameObject.GetInstanceID());
            }

            StartCoroutine(RefreshShotCD());
        }
    }

    private IEnumerator RefreshShotCD()
    {
        canShoot = false;
        yield return new WaitForSeconds(shotCoolDown);
        canShoot = true;
    }
}
