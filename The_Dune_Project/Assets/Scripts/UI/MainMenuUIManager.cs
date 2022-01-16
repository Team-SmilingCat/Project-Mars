using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
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
    // an apply & reset to default buttons for settings?
    
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolSlider;
    [SerializeField] private Slider musicVolSlider;
    [SerializeField] private Slider effectsVolSlider;
    [SerializeField] private Slider uiEffectsVolSlider;
    
    private void Start()
    {
        LoadAudio();
        HideSettings();
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

    private void HideSettings()
    {
        Assert.IsTrue(mainMenu != null && settingsMenu != null && visualsPage != null && audioPage != null && controlsPage != null, 
            "No settings ui page in MainMenuUIManager");

        settingsMenu.gameObject.SetActive(false);
        visualsPage.SetActive(false);
        audioPage.SetActive(false);
        controlsPage.SetActive(false);
        activeSettingsPage = null;
    }

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

    /* Audio slides (set up with the Audio Mixer, which needs to be assigned to EVERY audio source in the game) */
    private void LoadAudio()
    {
        Assert.IsTrue(masterVolSlider && musicVolSlider  && effectsVolSlider && uiEffectsVolSlider, 
            "No audio slider in MainMenuUIManager");
        
        float masterVolLvl = PlayerPrefs.GetFloat("masterVolume", 0.75f);
        float musicVolLvl = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        float effectsVolLvl = PlayerPrefs.GetFloat("effectsVolume", 0.50f);
        float uiEffectsVolLvl = PlayerPrefs.GetFloat("uiEffectsVolume", 0.50f);
        
        masterVolSlider.value = masterVolLvl;
        musicVolSlider.value = musicVolLvl;
        effectsVolSlider.value = effectsVolLvl;
        uiEffectsVolSlider.value = uiEffectsVolLvl;
        
        audioMixer.SetFloat ("masterVolume", Mathf.Log10(masterVolLvl) * 20);
        audioMixer.SetFloat ("musicVolume", Mathf.Log10(musicVolLvl) * 20);
        audioMixer.SetFloat ("effectsVolume", Mathf.Log10(effectsVolLvl) * 20);
        audioMixer.SetFloat ("uiEffectsVolume", Mathf.Log10(uiEffectsVolLvl) * 20);
    }
    
    public void SetMasterVolumeLevel(float masterVolLvl)
    {
        audioMixer.SetFloat ("masterVolume", Mathf.Log10(masterVolLvl) * 20);
        PlayerPrefs.SetFloat("masterVolume", masterVolLvl);
    }

    public void SetMusicVolumeLevel(float musicVolLvl)
    {
        audioMixer.SetFloat ("musicVolume", Mathf.Log10(musicVolLvl) * 20);
        PlayerPrefs.SetFloat("musicVolume", musicVolLvl);
    }
    
    public void SetEffectsVolumeLevel(float effectsVolLvl)
    {
        audioMixer.SetFloat ("effectsVolume", Mathf.Log10(effectsVolLvl) * 20);
        PlayerPrefs.SetFloat("effectsVolume", effectsVolLvl);
    }
 
    public void SetUIEffectsVolumeLevel(float uiEffectsVolLvl)
    {
        audioMixer.SetFloat ("uiEffectsVolume", Mathf.Log10(uiEffectsVolLvl) * 20);
        PlayerPrefs.SetFloat("uiEffectsVolume", uiEffectsVolLvl);
    }
}
