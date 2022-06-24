using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMatchPiece : MonoBehaviour
{

    public Collider tombLinker;
    public int uniqueID;
    public Material shinyMat;


    public PuzzleMatchPiece(Collider sensorCollider, int uid, Material mat)
    {
        this.tombLinker = sensorCollider;
        this.uniqueID = uid;
        this.shinyMat = mat;
    }
}



