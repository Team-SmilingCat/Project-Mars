using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLobbyBoss : MonoBehaviour
{
    [SerializeField] CompleteFourPuzzle completeFourPuzzle;
    [SerializeField] TriggerEvent triggerEvent;
    [SerializeField] GameObject cutsceneObject;
    [SerializeField] private int countRequirement;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && completeFourPuzzle.GetNumPieces() == countRequirement)
        {
            Debug.Log("played");
            cutsceneObject.SetActive(true);
            triggerEvent.OnNotify();
        }
        
    }
}
