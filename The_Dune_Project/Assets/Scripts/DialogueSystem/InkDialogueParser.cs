using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Ink.Runtime;
using TMPro;
using Cinemachine;
using System.Linq;

// This is a super bare bones example of how to play and display a ink story in Unity.
public class InkDialogueParser : MonoBehaviour {
    public static event Action<Story> OnCreateStory;
    [SerializeField]
	private TextAsset inkJSONAsset = null;
	public Story story {get; set;}

    [SerializeField] private AudioSource audioSource;

	[SerializeField] private Canvas canvas = null;
	[SerializeField] private TextMeshProUGUI textPrefab = null;
	[SerializeField] private Button buttonPrefab = null;
    [SerializeField] private List<AudioClip> audioClips;
    private Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();

    void Awake () {
        InitAudio();
	}

	public void StartStory () {
		story = new Story (inkJSONAsset.text);
        if(OnCreateStory != null) OnCreateStory(story);
		StartCoroutine(RefreshView());
	}
	
    private void InitAudio(){
        foreach(var i in audioClips){
            clipDictionary.Add(i.name.ToLower().Replace(" ",""), i);
        }
    }

    
    private void PlayClipName(string clipName){
        if(clipDictionary.TryGetValue(clipName.ToLower(), out var clip)){
            audioSource.PlayOneShot(clip);
        }
    }
	IEnumerator RefreshView () {
		// Remove all the UI on screen
		RemoveChildren ();
		
		// Read all the content until we can't continue any more
		while (story.canContinue) {
            while(audioSource.isPlaying){
                yield return null;
            }
			// Continue gets the next line of the story
			string text = story.Continue ();
			// This removes any white space from the text.
			text = text.Trim();
			// Display the text on screen!
			CreateContentView(text);

            foreach (var tag in story.currentTags){
                if(tag.StartsWith("Clip.")){
                    var clipName = tag.Substring("Clip.".Length, tag.Length - "Clip.".Length);
                    PlayClipName(clipName);
                }
                else if (tag.StartsWith("Camera")){
                    var cameraName = tag.Substring("Camera.".Length, tag.Length - "Camera.".Length);
                    var allCameras = FindObjectsOfType<CinemachineVirtualCamera>();
                    foreach(var cam in allCameras)
                    {
                        cam.Priority = cam.name == cameraName ? 100 : 10;
                    }
                }
            }
		}
		if(story.currentChoices.Count > 0) {
			for (int i = 0; i < story.currentChoices.Count; i++) {
				Choice choice = story.currentChoices [i];
				Button button = CreateChoiceView (choice.text.Trim ());
				// Tell the button what to do when we press it
				button.onClick.AddListener (delegate {
					OnClickChoiceButton (choice);
				});
			}
		}
		else {
			Button choice = CreateChoiceView("End of story.\nRestart?");
			choice.onClick.AddListener(delegate{
				StartStory();
			});
		}
	}

	void OnClickChoiceButton (Choice choice) {
		story.ChooseChoiceIndex (choice.index);
		StartCoroutine(RefreshView());
	}
	void CreateContentView (string text) {
		TextMeshProUGUI storyText = Instantiate (textPrefab) as TextMeshProUGUI;
		storyText.text = text;
		storyText.transform.SetParent (canvas.transform, false);
	}

	Button CreateChoiceView (string text) {
		// Creates the button from a prefab
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (canvas.transform, false);
		
		// Gets the text from the button prefab
		TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI> ();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
		layoutGroup.childForceExpandHeight = false;

		return choice;
	}

	void RemoveChildren () {
		int childCount = canvas.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			GameObject.Destroy (canvas.transform.GetChild (i).gameObject);
		}
	}


}