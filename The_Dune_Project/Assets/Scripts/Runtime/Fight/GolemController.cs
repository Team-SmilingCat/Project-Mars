using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemController : EnemyController
{
    private EnemyAnimatorManager enemyAnimatorManager;
    public bool enemyIsInteracting;

    [SerializeField] private List<Transform> potentialGoalPoints;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GlobalGameManager globalGameManager;

    [SerializeField] private Vector3 newPos;


    private void Start()
    {
        LoadAgentProperties(this);
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        enemyAnimatorManager.SetAnimBool("Awake", true);
        newPos = transform.position;
    }
    
    void Update()
    {
        base.Update();
    }

    public override void GoToTarget()
    {
        
    }

    public override void StepBackFromTarget()
    {
        
    }

    public override void FaceTarget()
    {
        throw new System.NotImplementedException();
    }

    public void MoveToARandomWayPoint()
    {
        enemyAnimatorManager.SetAnimBool("walking", true);
        Debug.Log(Vector3.Distance(transform.position, newPos));
        if (Vector3.Distance(transform.position, newPos) < 1.5f)
        {
            newPos = potentialGoalPoints[UnityEngine.Random.RandomRange(0, potentialGoalPoints.Count)].position;
            this.myAgent.SetDestination(newPos);   
        }
    }

    public void ShootSpears()
    {
        if (Vector3.Distance(transform.position, newPos) < 1.5f)
        {
            
        }
    }

    public bool isCloseEnough()
    {
        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            return true;
        }

        return false;
    }
}
