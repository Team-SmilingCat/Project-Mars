﻿using Fight;
using UnityEngine;
using UnityEngine.AI;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using Unity.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyController : MonoBehaviour
{
    [SerializeField] protected float lookRadius = 25f;
    [SerializeField] protected float timeToDeagrro = 5f;
    protected float aggroTimer;
    [SerializeField, ReadOnly] protected bool isAggroed = false;

    [SerializeField] protected float hitCooldown = 3.0f;
    protected float hitCooldownTimer;
    
    [SerializeField] protected Vector3 homeLocation;
    [SerializeField] protected NavMeshAgent myAgent;
    [SerializeField] protected Fighter myFighter;
    
    [ReadOnlyAttribute, SerializeField] protected Transform target;
    
    
    protected void LoadAgentProperties(EnemyController e)
    {
        target = FindObjectOfType<PlayerFighter>().transform;
        myAgent = GetComponent<NavMeshAgent>();
        myFighter = GetComponent<Fighter>();
        homeLocation = transform.position; // when player runs away, enemy goes back to their initial location
    }
    protected virtual void Update()
    {
        hitCooldownTimer = Mathf.Max( hitCooldownTimer - Time.deltaTime, 0.0f);
        aggroTimer = Mathf.Max( aggroTimer - Time.deltaTime, 0.0f);

        if (myFighter.curHealth <= 0)
        {
            Destroy(this);
        }
    }

    public abstract void GoToTarget();
    public abstract void FaceTarget();

    public abstract void StepBackFromTarget();

    public virtual void Attack()
    {
        Debug.Log("The enemy attempted to attack.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}