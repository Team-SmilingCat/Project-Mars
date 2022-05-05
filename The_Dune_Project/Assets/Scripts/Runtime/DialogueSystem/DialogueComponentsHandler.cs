using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Cinemachine;


public class DialogueComponentsHandler : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    //assumes cameras required for the dialogues are active in the scene
    public void HandleTags(Story story, List<CinemachineVirtualCamera> l)
    {
        Debug.Log("tag handle");
        foreach(var tag in story.currentTags)
        {
            switch(tag)
            {   
                case string s when tag.StartsWith("Clip."): 
                    dialogueManager.PlayClipName(ReturnClipName(s));
                    break;
                case string c when tag.StartsWith("Camera."): 
                    ReturnCamera(l, c);
                    break;
                default: 
                    Debug.Log("unrecognized tag has been initialized");
                    break;
            }
        }

    }

    private string ReturnClipName(string s)
    {
        var clipName = s.Substring("Clip.".Length, s.Length - "Clip.".Length);
        return clipName;
    }

    private void ReturnCamera(List<CinemachineVirtualCamera> l, string c)
    {
        var cameraName = c.Substring("Camera.".Length, c.Length - "Camera.".Length);
        foreach(var cam in l){
            cam.Priority = cam.name.Equals(cameraName) ? 100 : 10;
        }    
    }

}