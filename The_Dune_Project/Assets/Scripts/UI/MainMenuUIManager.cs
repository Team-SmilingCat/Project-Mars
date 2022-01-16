using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private VerticalLayoutGroup mainMenu;
    [SerializeField] private VerticalLayoutGroup settingsMenu;
    
    [Header("Settings Pages")]
    [SerializeField] private GameObject visualsPage;
    [SerializeField] private GameObject audioPage;
    [SerializeField] private GameObject controlsPage;
    private GameObject activeSettingsPage;
    
    private void Start()
    {
        if (mainMenu == null || settingsMenu == null || visualsPage == null || audioPage == null || controlsPage == null)
        {
            Debug.LogError("No settings ui page in MainMenuUIManager");
            return;
        }
        
        settingsMenu.gameObject.SetActive(false);
        visualsPage.SetActive(false);
        audioPage.SetActive(false);
        controlsPage.SetActive(false);
        activeSettingsPage = null;
    }

    private void Update()
    {
        if (settingsMenu.IsActive())
        {
            if (activeSettingsPage == audioPage)
            {
                
            }
        }
        else
        {
            if (!mainMenu.IsActive())
            {
                mainMenu.gameObject.SetActive(true);
            }
        }
    }

    /* Main Menu Buttons */
    
    public void OnContinueButtonClicked()
    {
        SceneManager.LoadScene("Scenes/MarsTest1"); // TODO: get scene the player is saved in
    }
    
    public void OnNewGameButtonClicked() // TODO: confirmation pop up
    {
        SceneManager.LoadScene("Scenes/MarsTest1"); // TODO: get starting game scene
    }
    
    public void OnSettingsButtonClicked()
    {
        mainMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
        SetPageActive("visuals");
    }
    
    public void OnQuitButtonClicked() => Application.Quit(); // TODO: confirmation pop up
    
    /* Settings */

    public void OnVisualsButtonClicked() => SetPageActive("visuals");
    
    public void OnAudioButtonClicked() => SetPageActive("audio");
    
    public void OnControlsButtonClicked() => SetPageActive("controls");

    public void OnBackButtonClicked() => SetPageActive("back");

    private void SetPageActive(string pageName)
    {
        if (visualsPage == null || audioPage == null || controlsPage == null)
        {
            Debug.LogError("No settings ui page in MainMenuUIManager");
            return;
        }
        
        visualsPage.SetActive(false);
        audioPage.SetActive(false);
        controlsPage.SetActive(false);

        switch (pageName)
        {
            case "visuals":
                visualsPage.SetActive(true);
                activeSettingsPage = visualsPage;
                break;
            
            case "audio":
                audioPage.SetActive(true);
                activeSettingsPage = audioPage;
                break;
            case "controls":
                controlsPage.SetActive(true);
                activeSettingsPage = controlsPage;
                break;
            case "back":
                settingsMenu.gameObject.SetActive(false);
                mainMenu.gameObject.SetActive(true);
                activeSettingsPage = null;
                break;
        }
    }

    // an apply & reset to default buttons for settings?
    
}
