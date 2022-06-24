using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TombBehaviour : MonoBehaviour
{
    [SerializeField] private Material shinyMat;
    [SerializeField] private Collider associatedSwitch;

    public PuzzleMatchPiece puzzleMatchPiece;
    [SerializeField] private CompleteFourPuzzle completeFourPuzzle;
    private int associatedSensorID;
    private GameObject light;
  
    private void Start()
    {
        puzzleMatchPiece = new PuzzleMatchPiece(associatedSwitch,
            GetInstanceID(), shinyMat);

        associatedSensorID = puzzleMatchPiece.tombLinker.gameObject.GetInstanceID();
        light = associatedSwitch.gameObject.transform.GetChild(0).gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sensor") && 
            associatedSwitch.GetInstanceID().Equals(other.GetInstanceID()))
        {
            completeFourPuzzle.incrementDonePieceCount();
            gameObject.GetComponent<MeshRenderer>().material = shinyMat;
            light.SetActive(true);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}

