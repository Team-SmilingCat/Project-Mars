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

	private Story currentStory;
	[SerializeField] private TextMeshProUGUI textPrefab = null;
	[SerializeField] private Button buttonPrefab = null;

	[Header("Script Components")]
	[SerializeField] private DialogueComponentsHandler dialogueComponentsHandler;

	public void StartStory (Story s, AudioSource a, Canvas c, List<CinemachineVirtualCamera> l, GameObject d, TextMeshProUGUI t, List<Button> b) 
	{
        if(OnCreateStory != null) OnCreateStory(s);
		currentStory = s;
		StartCoroutine(RefreshView(currentStory, a, c, l, d, t, b));
	}

	IEnumerator RefreshView (Story s, AudioSource a, Canvas c, List<CinemachineVirtualCamera> l, GameObject d, TextMeshProUGUI t, List<Button> b) 
	{
		// Remove all the UI associated with the dialogue canvas on screen
		RemoveChildren (c, t, b);
		//display regular text
		while (s.canContinue)
		{
            while(a.isPlaying)
			{
                yield return null;
            }
			string text = s.Continue ();
			text = text.Trim();
			//display text on screen
			CreateContentView(text, c, d, t);
			dialogueComponentsHandler.HandleTags(s, l);
		}
		//display choices in dialogue
		if(s.currentChoices.Count > 0) 
		{
			for (int i = 0; i < s.currentChoices.Count; i++) 
			{
				Choice choice = s.currentChoices [i];	
				//b.ElementAt(i) = CreateChoiceView (choice.text.Trim (), c, b, i);
				// Tell the button what to do when we press it
				b.ElementAt(i).gameObject.SetActive(true);
				b.ElementAt(i).GetComponentInChildren<TextMeshProUGUI>().text =  choice.text.Trim();
				b.ElementAt(i).onClick.AddListener (delegate 
				{
					OnClickChoiceButton (choice, s, a, c, l, d, t, b);
				});
			}
		}
		else
		{
			s.ResetState();
			ResetCameraPriorities(l);
		}
		
	}

	private void OnClickChoiceButton (Choice choice, Story s, AudioSource a, Canvas c, List<CinemachineVirtualCamera> l, GameObject d, TextMeshProUGUI t, List<Button> b) 
	{
		try{
			if(s.currentChoices.Count <= 0)
			{
				return;
			}
			else
			{
				s.ChooseChoiceIndex(choice.index);
				StartCoroutine(RefreshView(s, a, c, l, d, t, b));
			}
		}
		catch(Exception e)
		{
			throw new Exception(e.ToString());
		}
	}

	//displays the text on screen
	private void CreateContentView (string text, Canvas c, GameObject d, TextMeshProUGUI t) 
	{
		t.text = text;
	}


	private void RemoveChildren (Canvas c, TextMeshProUGUI t, List<Button> b) 
	{
		t.text = "";
		foreach(Button button in b)
		{
			button.GetComponentInChildren<TextMeshProUGUI>().text = "";
			button.gameObject.SetActive(false);
		}
	}

	private void ResetCameraPriorities(List<CinemachineVirtualCamera> c)
	{
		foreach(var cam in c)
		{
			cam.Priority = 10;
		}

	}

	


}