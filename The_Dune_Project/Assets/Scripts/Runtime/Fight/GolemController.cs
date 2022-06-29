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


    private void Start()
    {
        LoadAgentProperties(this);
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        enemyAnimatorManager.SetAnimBool("Awake", true);
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
        Debug.Log("wef");
        var newPos = potentialGoalPoints[UnityEngine.Random.RandomRange(0, potentialGoalPoints.Count)].position;
        this.myAgent.SetDestination(newPos);
    }
}
