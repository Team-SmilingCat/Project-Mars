using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using CleverCrow.Fluid.BTs;
using CleverCrow.Fluid.BTs.Trees;
using TaskStatus = CleverCrow.Fluid.BTs.Tasks.TaskStatus;


public class AutomataBehaviourTree : MonoBehaviour
{
    [SerializeField] private BehaviorTree automataTree;

    private MeleeEnemyController controller;

    private void Start()
    {
        controller = GetComponent<MeleeEnemyController>();
    }

    private void Awake()
    {
        automataTree = new BehaviorTreeBuilder(gameObject)
            .Sequence("Chase Player")
            .Do("Moving", () =>
            {
                controller.GoToTarget();
                return TaskStatus.Success;
            })
            .End()
            .Sequence("Attack Player")
            .Do("Bash", () =>
            {
                controller.PerformBaseMeleeAtk();
                return TaskStatus.Success;
            })
            .End()
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
