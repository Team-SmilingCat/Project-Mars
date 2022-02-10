using System;
using UnityEngine;

public class ChaseState : BaseState
{
    private Monster enemy;
    public ChaseState(Monster Monster) : base(Monster.gameObject)
    {
        this.enemy = Monster;
    }
    
    public override Type Action()
    {
        if (enemy.isAudioEnabled)
        {
            enemy.gameObject.GetComponent<AudioSource>().clip = enemy.GetComponent<Monster>().scream;
            enemy.gameObject.GetComponent<AudioSource>().Play();
            enemy.isAudioEnabled = false;
        }

        if (this.enemy.Target.Equals(null))
        {
            Debug.Log("going back to wandering");
            return typeof(WanderState);
        }
        //get lookat from ray
        this.transform.LookAt(new Vector3(this.enemy.Target.position.x, transform.position.y, this.enemy.Target.position.z));
        this.transform.Translate(Vector3.forward * Time.deltaTime * MonsterState.currentSpeed);
        
        if (enemy.isHittingWall)
        {
            Debug.Log("Player is hiding so returning to wandering");
            return typeof(WanderState);
        }
        
        var distance = Vector3.Distance(this.transform.position, this.enemy.Target.transform.position);
        if (distance <= MonsterState.AttackRange)
        {
            Debug.Log("Attacking player");
            //if we are in range change state to attack
            return typeof(AttackState);
        }

        else if (distance >= 40f)
        {
            Debug.Log("Returning back to wandering after chasing");
            return typeof(WanderState);
        }

        return null;
    } 
}