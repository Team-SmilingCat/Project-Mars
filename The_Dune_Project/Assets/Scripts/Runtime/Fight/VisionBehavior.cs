using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CleverCrow.Fluid.BTs;
using CleverCrow.Fluid.BTs.Trees;
using TaskStatus = CleverCrow.Fluid.BTs.Tasks.TaskStatus;

public class VisionBehavior : MonoBehaviour
{
    [SerializeField] private BehaviorTree visionTree;
    private VisionFlyerController controller;


    void Start()
    {
        controller = GetComponent<VisionFlyerController>();
        visionTree = new BehaviorTreeBuilder(gameObject)
            .Selector("choices")
            .Sequence("fly towards player").
            Do("go to player", () =>
            {
                controller.GoToTarget();
                return TaskStatus.Success;
            })
            .End()
            .Build();
    }

    void Update()
    {
        visionTree.Tick();
        
    }
}
