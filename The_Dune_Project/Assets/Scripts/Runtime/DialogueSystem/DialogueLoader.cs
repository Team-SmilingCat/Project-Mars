using System.Collections;
using System.Collections.Generic;
using Scriptable_Objects;
using UnityEngine;
using Cinemachine;
using Ink.Runtime;

public class DialogueLoader : MonoBehaviour
{
    //attached to where the dialogue trigger lies. This class loads up the dialogue 
    //when the player wants to engage. all parameters required to pass to the parser is 
    //within the scriptable object.
    [Header("dialogue manager with the canvas and ui elements")]        
    [SerializeField] private DialogueManager dialogueManager;

    [Header("ink file parser for the scriptable object for dialogue")]
    [SerializeField] InkDialogueParser inkDialogueParser;
    [SerializeField] protected DialogueRequirement dialogRequirement;

    [Header("Dialogue Cameras specific to this dialogue scene")]
    [SerializeField] List<CinemachineVirtualCamera> cameraList;

    [Header("camera of the main player")]
    [SerializeField] private CinemachineVirtualCamera thirdPersonCam;

    [Header("Parameter checks")]
    [SerializeField] bool isInDialogue;
    protected Story requiredDialogue;


    
    void Awake()
    {
        InitAudio();
    }

    private void InitAudio()
    {
        foreach(var i in dialogRequirement.audiosAssociatedWithDialogue)
        {
            dialogueManager.GetAudioLibrary().Add(i.name.ToLower().Replace(" ",""), i);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isInDialogue = false;
        requiredDialogue = new Story(dialogRequirement.inkDialogueFile.text);
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag.Equals("Player"))
        {
            dialogueManager.CheckDialoguebox();
            inkDialogueParser.StartStory(
            requiredDialogue,
            dialogueManager.GetAudioSource(),
            dialogueManager.GetDialogueCanvas(),
            cameraList,
            dialogueManager.GetDialogueBox(),
            dialogueManager.GetDialogueText(),
            dialogueManager.GetChoicesButtons()
            );
        }
    }

    private void OnTriggerExit(Collider other)
    {
        dialogueManager.CheckDialoguebox();
        
    }
}