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
	[SerializeField] private TextMeshProUGUI textPrefab = null;
	[SerializeField] private Button buttonPrefab = null;
	[SerializeField] private DialogueComponentsHandler dialogueComponentsHandler;
	public bool isFinishedParsing;

	void Awake(){
		isFinishedParsing = false;
	}

	public void StartStory (Story s, AudioSource a, Canvas c, List<CinemachineVirtualCamera> l) 
	{
		isFinishedParsing = true;
        if(OnCreateStory != null) OnCreateStory(s);
		StartCoroutine(RefreshView(s, a, c, l));
	}

	IEnumerator RefreshView (Story s, AudioSource a, Canvas c, List<CinemachineVirtualCamera> l) 
	{
		// Remove all the UI associated with the dialogue canvas on screen
		RemoveChildren (c);
		while (s.canContinue)
		{
            while(a.isPlaying)
			{
                yield return null;
            }
			string text = s.Continue ();
			text = text.Trim();
			//display text on screen
			CreateContentView(text, c);
			dialogueComponentsHandler.HandleTags(s, l);
		}
		if(s.currentChoices.Count > 0) 
		{
			for (int i = 0; i < s.currentChoices.Count; i++) 
			{
				Choice choice = s.currentChoices [i];
				Button button = CreateChoiceView (choice.text.Trim (), c);
				// Tell the button what to do when we press it
				button.onClick.AddListener (delegate 
				{
					OnClickChoiceButton (choice, s, a, c, l);
				});
			}
		}
		else {
			isFinishedParsing = true;
		}
	}

	void OnClickChoiceButton (Choice choice, Story s, AudioSource a, Canvas c, List<CinemachineVirtualCamera> l) 
	{
		s.ChooseChoiceIndex (choice.index);
		StartCoroutine(RefreshView(s, a, c, l));
	}
	void CreateContentView (string text, Canvas c) 
	{
		TextMeshProUGUI storyText = Instantiate (textPrefab) as TextMeshProUGUI;
		storyText.text = text;
		storyText.transform.SetParent (c.transform, false);
	}

	Button CreateChoiceView (string text, Canvas c) 
	{
		// Creates the button from a prefab
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (c.transform, false);
		
		// Gets the text from the button prefab
		TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI> ();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
		layoutGroup.childForceExpandHeight = false;

		return choice;
	}

	void RemoveChildren (Canvas c) 
	{
		int childCount = c.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) 
		{
			GameObject.Destroy (c.transform.GetChild (i).gameObject);
		}
	}


}