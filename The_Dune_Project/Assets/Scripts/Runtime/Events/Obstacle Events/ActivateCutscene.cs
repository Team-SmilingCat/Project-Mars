using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActivateCutscene : MonoBehaviour
{
	[SerializeField] private GlobalGameManager globalGameManager;
    [SerializeField] private PlayableDirector cutscene;
    private bool hasPlayed;
    
    [SerializeField] private GameObject HUD;

    private void Start()
    {
        hasPlayed = false;
    }

    public void ExecuteEvent()
    {
        if (globalGameManager.currentState != GlobalGameManager.GameStates.MainLoop) return;
        hasPlayed = true;
        cutscene.Play();
        globalGameManager.SwitchStates(GlobalGameManager.GameStates.InCutScene);
        CheckForUI();
    }
    
    /* functions that can be flagged in the timeline */
    public void StopCutScene()
    {
        globalGameManager.SwitchStates(GlobalGameManager.GameStates.MainLoop);
        CheckForUI();
        cutscene.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {  
        if (other.tag.Equals("Player") && !hasPlayed)
        {
            ExecuteEvent();
        }
    }
    
    private void CheckForUI()
    {
        if (globalGameManager.currentState == GlobalGameManager.GameStates.MainLoop && HUD.activeSelf == false)
        {
            HUD.SetActive(true);
        }
        else if (globalGameManager.currentState == GlobalGameManager.GameStates.InCutScene && HUD.activeSelf)
        {
            HUD.SetActive(false);
        }
    }
}
