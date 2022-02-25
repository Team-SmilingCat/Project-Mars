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
            .Selector()
            .Sequence("move and attack")
                .Condition("Player is in range", () =>
                {
                    return controller.PlayerIsInRangeOfMe();
                })
                .Do("go to the player", () =>
                {
                    controller.GoToTarget();
                    return TaskStatus.Success;
                })
                .Do("attack", () =>
                {
                    controller.PerformBaseMeleeAtk();
                    return TaskStatus.Success;
                })
            .Build();
    }

    private void Update()
    {
        automataTree.Tick();
    }
}
