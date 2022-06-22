using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CompleteFourPuzzle : MonoBehaviour
{
    [SerializeField] private List<PuzzleMatchPiece> listOfPieces;
    
    public event Action<CompleteFourPuzzle, int> puzzleEvent;
    
    private void Start()
    {
        
    }

}

public class PuzzleMatchPiece
{

    public Collider tombLinker
    {
        get => tombLinker;
        set => tombLinker = value;
    }
    public int uniqueID
    {
        get => uniqueID;
        set => uniqueID = value;

    }
    public Material shinyMat
    {
        get => shinyMat;
        set => shinyMat = value;
    }
    

    public PuzzleMatchPiece(Collider sensorCollider, int uid, Material mat)
    {
        this.tombLinker = sensorCollider;
        this.uniqueID = uid;
        this.shinyMat = mat;
    }
}

