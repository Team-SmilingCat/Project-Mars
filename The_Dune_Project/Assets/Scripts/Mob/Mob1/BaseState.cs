using System;
using UnityEngine;

public abstract class BaseState
{

    protected GameObject myGameObject;
    protected Transform transform;

    public BaseState(GameObject gameObject)
    {
        this.myGameObject = gameObject;
        this.transform = this.myGameObject.transform;

    }

    //function that we will give to every state
    public abstract Type Action();

    
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

}
