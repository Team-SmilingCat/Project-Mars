using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent triggeredEvent;

    public void OnNotify()
    {
        triggeredEvent.Invoke();
    }
}
