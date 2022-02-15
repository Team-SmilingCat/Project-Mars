using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using CleverCrow.Fluid.BTs;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;


public class AutomataBehaviourTree : MonoBehaviour
{
    [SerializeField] private BehaviorTree automataTree;

    private EnemyController enemyController;

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    private void Awake()
    {
        automataTree = new BehaviorTreeBuilder(gameObject)
            .Sequence("Idle")
            .Do("Original Action 1", () =>
            {
                //call the action leaf node here.
                return TaskStatus.Success;
            })
            .End()
            .Build();
    }

    private void Update()
    {
        automataTree.Tick();
    }
}
