using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> stateList;
    public BaseState CurrentState { get; private set; }
    public event Action<BaseState> OnStateChanged;
    
    private void Update()
    {
        //if current state none, need to start in a state at the beginning
        if (CurrentState == null)
        {
            this.CurrentState = this.stateList.Values.First();
        }
        //get the next state
        var nextState = this.CurrentState?.Action();

        if ( (nextState != null) && (nextState != this.CurrentState?.GetType()) )
        {
            this.SwitchToNewState( nextState );
        }
    }
    
    public void SetStates(Dictionary<Type, BaseState> states)
    {
        this.stateList = states;

    }
    
    private void SwitchToNewState( Type nextState )
    {
        this.CurrentState = this.stateList[ nextState ];
        OnStateChanged?.Invoke(this.CurrentState);

    }

}
