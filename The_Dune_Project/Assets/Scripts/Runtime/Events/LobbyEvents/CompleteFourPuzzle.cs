using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CompleteFourPuzzle : MonoBehaviour
{
    [SerializeField] TriggerEvent triggerEvent;
    [SerializeField] private int numberOfPiecesDone;

    public event Action<CompleteFourPuzzle, int> puzzleEvent;

    public void incrementDonePieceCount()
    {
        numberOfPiecesDone++;
    }

    public int GetNumPieces()
    {
        return numberOfPiecesDone;
    }
 
    
}

