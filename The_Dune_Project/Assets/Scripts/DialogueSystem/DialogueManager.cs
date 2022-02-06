using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();
    
    public Canvas canvas;
    public GameObject buttonPrefab;
    void Start()
    {

    }

    public Dictionary<string, AudioClip> GetAudioLibrary()
    {
        return clipDictionary;
    }

    public AudioSource GetAudioSource(){
        return audioSource;
    }

    public void PlayClipName(string clipName)
    {
        if(clipDictionary.TryGetValue(clipName.ToLower(), out var clip))
        {
            Debug.Log("audio has been played");
            audioSource.PlayOneShot(clip);
        }
    }
    

}