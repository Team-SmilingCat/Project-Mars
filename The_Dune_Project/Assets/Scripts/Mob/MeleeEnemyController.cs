using System.Collections;
using System.Collections.Generic;
using Fight;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyController : EnemyController
{
    private EnemyAnimatorManager enemyAnimatorManager;
    private Animator meleeAnimator;
    
    private void Start()
    {
        LoadAgentProperties(this);
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        meleeAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        base.Update();
    }

    public override void GoToTarget()
    {
        this.myAgent.SetDestination(target.position);
        meleeAnimator.SetBool("walking", true);
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
        if (!(distance <= myAgent.stoppingDistance)) return;
        FaceTarget();
        Fighter targetFighter = target.GetComponent<Fighter>();
        if (targetFighter != null && hitCooldownTimer <= 0.0f)
        {
            enemyAnimatorManager.PlayTargetAnimation("smash", true);
            targetFighter.TakeDamage(10); // TODO
            hitCooldownTimer = hitCooldown;
        }
    }

    public void ReturnToOriginalLocation()
    {
        if (isAggroed && aggroTimer <= 0)
        {
            isAggroed = false;
            myAgent.SetDestination(homeLocation);
            meleeAnimator.SetBool("walking", true);
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
}
