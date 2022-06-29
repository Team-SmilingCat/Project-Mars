using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CleverCrow.Fluid.BTs;
using CleverCrow.Fluid.BTs.Trees;
using TaskStatus = CleverCrow.Fluid.BTs.Tasks.TaskStatus;

public class TempleGolemBehavior : MonoBehaviour
{
    [SerializeField] private BehaviorTree golemnTree;

    private GolemController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<GolemController>();
        golemnTree = new BehaviorTreeBuilder(gameObject)
            .Selector("choices")
            .Sequence("move and attack")
            .Do("go to the player", () =>
            {
                controller.MoveToARandomWayPoint();
                return TaskStatus.Success;
            })
           
            .WaitTime(5f)
            .End()
            .Build();
    }

    // Update is called once per frame
    void Update()
    {
        golemnTree.Tick();
    }
}
