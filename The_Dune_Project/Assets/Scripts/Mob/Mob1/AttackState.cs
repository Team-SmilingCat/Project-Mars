using System;
using UnityEngine;

public class AttackState : BaseState
{
    //params
    private float attackReadyTimer;
    private Monster currEnemy;
    

    public AttackState(Monster Monster) : base(Monster.gameObject)
    {
        this.currEnemy = Monster;
    }
    
    public override Type Action()
    {
        if (this.currEnemy.Target.Equals(null))
        {
            //go back to going around
            return typeof(WanderState);
        }
        var distance = Vector3.Distance(this.transform.position, currEnemy.Target.transform.position);
        if (distance >= 5f)
        {
            return typeof(ChaseState);
        }
        //else call function
            this.currEnemy.Attack();
            return null;
        
    }
    

}
