using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerActions : MonoBehaviour
{
    protected bool isDoingAction;
    
    public abstract void Action();
}
