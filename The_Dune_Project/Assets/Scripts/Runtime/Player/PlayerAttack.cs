using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scriptable_Objects;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AnimatorManager animatorManager;
    [SerializeField] private PlayerInputHandle playerInputHandle;
    private Animator animator;

    [Header("combat settings")]
    [SerializeField] private LayerMask targetableLayer;
    [SerializeField] private float detectableRadius;
    [SerializeField] private float maxDistance;
    [SerializeField] private Vector3 direction;

    [Header("attack settings")]
    private string prevAtk;

    [Header("hit targets")]
    [SerializeField] RaycastHit[] listOfEnemiesinRange;
    [SerializeField] Transform closestTarget;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        direction = gameObject.transform.forward;
    }

    public void handleMeleeAttack(MeleeWeapon weapon)
    {
        RotateToTarget();
        animatorManager.PlayTargetAnimation(weapon.atk1, true);
        prevAtk = weapon.atk1;
    }

    public void HandleHeavyMeleeAttack(MeleeWeapon weapon)
    {
        animatorManager.PlayTargetAnimation(weapon.atk4, true);
        prevAtk = weapon.atk4;
    }

    public void handleMeleeAttackSequence(MeleeWeapon weapon)
    {
        if (playerInputHandle.flagCombo)
        {
            animatorManager.animator.SetBool("canCombo", false);
            if (prevAtk.Equals(weapon.atk1))
            {
                RotateToTarget();
                animatorManager.PlayTargetAnimation(weapon.atk2, true);
                prevAtk = weapon.atk2;
            } 
            else if (prevAtk.Equals(weapon.atk2))
            {
                RotateToTarget();
                animatorManager.PlayTargetAnimation(weapon.atk3, true);
            }
        }
    }

    public void RotateToTarget()
    {
        GetNearestTarget();
        if(closestTarget != null)
        {
            var rotationVector = new Vector3(closestTarget.position.x, transform.position.y, closestTarget.position.z);
            transform.LookAt(rotationVector);


        }

    }

    private Transform GetNearestTarget()
    {
        List<RaycastHit> nearestEnemies = Physics.SphereCastAll(transform.position,
            detectableRadius, direction, maxDistance, targetableLayer,
            QueryTriggerInteraction.Collide).OfType<RaycastHit>().ToList();

        if(!nearestEnemies.Any())
        {
            return null;
        }
        else
        {
            float nearestDist = Mathf.Infinity;

            foreach (RaycastHit hit in nearestEnemies)
            {
                Vector3 directionToTarget = hit.transform.position - transform.position;
                float distanceSquared = directionToTarget.sqrMagnitude;
                if (distanceSquared < nearestDist)
                {
                    nearestDist = distanceSquared;
                    closestTarget = hit.transform;
                }
            }
        }
        return closestTarget;
    }


    void OnDrawGizmosSelected()
    {
        Vector3 debugVec3 = new Vector3(gameObject.transform.position.x,
        gameObject.transform.position.y,
        gameObject.transform.position.z);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(debugVec3, detectableRadius);
    }
}
