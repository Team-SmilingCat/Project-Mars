using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionFlyerController : EnemyController
{
    private void Start()
    {
        LoadAgentProperties(this);
    }

    public override void FaceTarget()
    {
        throw new System.NotImplementedException();
    }

    public override void GoToTarget()
    {
        transform.LookAt(this.target);
        var targetPos = new Vector3 (transform.position.x, transform.position.y + 5f
            ,transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position,
            targetPos, Time.deltaTime * myFighter.GetEnemySpeed());
    }

    public override void StepBackFromTarget()
    {
        throw new System.NotImplementedException();
    }

    public void AlertGolemn()
    {

    }

}
