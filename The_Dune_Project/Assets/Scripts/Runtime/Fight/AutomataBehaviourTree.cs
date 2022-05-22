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
        .Selector("choices")
                .Sequence("too close")
                    .Condition("Player is too close, knock back him", () =>
                    {
                        return controller.PlayerIsTooClose();
                    })
                    .Do("knockback the player", () =>
                        {
                            controller.KnockbackPlayerForSpace();
                            return TaskStatus.Success;
                        })
                .End()
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
                        controller.Attack();
                        return TaskStatus.Success;
                    })
                    .WaitTime(1.5f)
                .End()
                .Sequence("reset range")
                    .Condition("Lost player", () => !controller.PlayerIsInRangeOfMe())
                    .Do("go back to original location", () =>
                    {
                        controller.ReturnToOriginalLocation();
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

