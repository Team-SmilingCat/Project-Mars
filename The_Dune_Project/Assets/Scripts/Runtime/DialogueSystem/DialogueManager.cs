using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class DialogueManager : MonoBehaviour
{

    [Header("dialogue assets")]
    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] List<Button> choices;

    [Header("Manager Components")]
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();
    
    public GameObject buttonPrefab;
    void Start()
    {

    }

    public Dictionary<string, AudioClip> GetAudioLibrary()
    {
        return clipDictionary;
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    public Canvas GetDialogueCanvas()
    {
        return this.dialogueCanvas;
    }

    public GameObject GetDialogueBox()
    {
        return this.dialogueBox;
    }

    public void CheckDialoguebox()
    {
        if(!this.dialogueBox.active)
        {
            this.dialogueBox.SetActive(true);
        } 
        else
        {
            this.dialogueBox.SetActive(false);  
        }
    }

    public TextMeshProUGUI GetDialogueText()
    {
        return this.dialogueText;
    }

    public List<Button> GetChoicesButtons(){
        return this.choices;
    }

    public Button ReturnAChoiceSelection(int index)
    {
        return this.choices.ElementAt(index);
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