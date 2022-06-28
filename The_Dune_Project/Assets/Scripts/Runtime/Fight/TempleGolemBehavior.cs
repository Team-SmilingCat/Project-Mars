using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CleverCrow.Fluid.BTs;
using CleverCrow.Fluid.BTs.Trees;
using TaskStatus = CleverCrow.Fluid.BTs.Tasks.TaskStatus;

public class TempleGolemBehavior : MonoBehaviour
{
    [SerializeField] private BehaviorTree golemnTree;

    private MeleeEnemyController controller;
    // Start is called before the first frame update
    void Start()
    {
        golemnTree = new BehaviorTreeBuilder(gameObject)
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

    // Update is called once per frame
    void Update()
    {
        golemnTree.Tick();
    }
}
