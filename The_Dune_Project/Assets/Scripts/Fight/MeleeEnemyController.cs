using System.Collections;
using System.Collections.Generic;
using Fight;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyController : EnemyController
{
    private EnemyAnimatorManager enemyAnimatorManager;
    public bool enemyIsInteracting;
    [SerializeField] private float knockbackConditionLimit;
    
    private void Start()
    {
        LoadAgentProperties(this);
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
    }

    private void Update()
    {
        base.Update();
    }

    private void LateUpdate()
    {
        enemyIsInteracting = enemyAnimatorManager.GetBoolData("isInteracting");
        
    }

    public override void GoToTarget()
    {
        if (enemyAnimatorManager.GetBoolData("isInteracting"))
        {
            return;
        }
        FaceTarget();
        this.myAgent.SetDestination(target.position);
        enemyAnimatorManager.SetAnimBool("walking", true);
        aggroTimer = timeToDeagrro;
        isAggroed = true;
    }

    public override void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction = new Vector3(direction.x, 0, direction.z);
        if (direction.sqrMagnitude > 0.25f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    public void PerformBaseMeleeAtk()
    {        
        float distance = Vector3.Distance(target.position, transform.position);
        Fighter targetFighter = target.GetComponent<Fighter>();
        if (targetFighter != null && hitCooldownTimer <= 0.0f)
        {
            enemyAnimatorManager.SetAnimBool("walking", false);
            enemyAnimatorManager.PlayTargetAnimation("smash", true);// TODO
            hitCooldownTimer = hitCooldown;
        }
    }

    public void KnockbackPlayerForSpace()
    {
        Fighter targetFighter = target.GetComponent<Fighter>();
        FaceTarget();
        if (targetFighter != null && hitCooldownTimer <= 0.0f)
        {
            enemyAnimatorManager.SetAnimBool("walking", false);
            enemyAnimatorManager.PlayTargetAnimation("slash", true);
            //perform knockback to the player as an aniation event
            hitCooldownTimer = hitCooldown;
        }
    }

    public void ReturnToOriginalLocation()
    {
        if (isAggroed && aggroTimer <= 0)
        {
            isAggroed = false;
            myAgent.SetDestination(homeLocation);
            enemyAnimatorManager.SetAnimBool("walking", false);
        }
    }

    public bool PlayerIsInRangeOfMe()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PlayerIsTooClose()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if ((distance <= knockbackConditionLimit))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void StepBackFromTarget()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 knockBackRange = transform.TransformDirection(Vector3.forward) * knockbackConditionLimit;
        Gizmos.DrawRay(transform.position, knockBackRange);
    }
}
