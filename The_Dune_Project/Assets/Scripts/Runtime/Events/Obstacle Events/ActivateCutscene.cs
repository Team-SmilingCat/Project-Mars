using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActivateCutscene : MonoBehaviour
{
    [SerializeField] private PlayableDirector cutscene;

    public void ExecuteEvent()
    {
        cutscene.gameObject.SetActive(true);
    }
    
    /* functions that can be flagged in the timeline */
    public void StopCutScene()
    {
        cutscene.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {  
        if (other.tag.Equals("Player"))
        {
            ExecuteEvent();
        }
    }
}
