using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] private Canvas canvas;

    void Start()
    {
        
    }



    public virtual void TriggerDialogue(){
        Debug.Log("Dialogue initiated by the player");
    }

    public void setCanvas(Canvas c){
        this.canvas = c;
    }

    public Canvas getCanvas(){
        return this.canvas;
    }
}
