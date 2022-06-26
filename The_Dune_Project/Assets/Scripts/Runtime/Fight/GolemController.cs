using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemController : EnemyController
{
    private EnemyAnimatorManager enemyAnimatorManager;
    public bool enemyIsInteracting;

    private void Start()
    {
        LoadAgentProperties(this);
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
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
}
