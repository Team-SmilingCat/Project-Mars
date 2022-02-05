using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private VerticalLayoutGroup mainMenu;
    [SerializeField] private VerticalLayoutGroup settingsMenu;
    [SerializeField] private PopUp popUpPrefab;
    private PopUp curPopUp;
    
    [Header("Settings Pages")]
    [SerializeField] private GameObject visualsPage;
    [SerializeField] private GameObject audioPage;
    [SerializeField] private GameObject controlsPage;
    private GameObject activeSettingsPage;
    // cache default settings to be able to revert to default
    
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolSlider;
    [SerializeField] private Slider musicVolSlider;
    [SerializeField] private Slider effectsVolSlider;
    [SerializeField] private Slider uiEffectsVolSlider;

    [Header("Visuals")] 
    [SerializeField] private List<ResolutionStruct> resolutionOptions;
    [SerializeField] private TMPro.TMP_Dropdown resolutionDropdownUI;
    [SerializeField] private TMPro.TMP_Dropdown graphicsQualityDropdownUI;
    [Serializable] struct ResolutionStruct { public int width, height; }
    
    private void Start()
    {
        LoadAudio();
        HideSettings();
        SetUpVisuals();
    }

    public void Escape(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (curPopUp.IsPopUpActive())
            {
                curPopUp.OnClickPopUpButton(false);
            }
            else
            {
                OnBackButtonClicked();
            } 
        }
    }

    private void Update()
    {
        if (!settingsMenu.IsActive() && !mainMenu.IsActive()) // fallback
        {
            mainMenu.gameObject.SetActive(true);
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

    private void SetUpVisuals()
    {
        /* Resolution */
        var curRes = Screen.currentResolution;
        int curResIndex = 0;
        
        Screen.SetResolution(curRes.width, curRes.height, Screen.fullScreenMode); // TODO: is this player pref stored?

        // dropdown naming
        if (resolutionDropdownUI != null)
        {
            List<string> resolutionNames = new List<string>();
            for(int i = 0; i < resolutionOptions.Count; i++)
            {
                var res = resolutionOptions[i];
                resolutionNames.Add(res.width + " x " + res.height);
            
                if ((Screen.width == res.width) && (Screen.height == res.height))
                    curResIndex = i;
            }
            resolutionDropdownUI.ClearOptions();
            resolutionDropdownUI.AddOptions(resolutionNames);
            resolutionDropdownUI.value = curResIndex;
            resolutionDropdownUI.RefreshShownValue();
        }

        /* Graphics Quality */
        if (graphicsQualityDropdownUI != null)
        {
            graphicsQualityDropdownUI.ClearOptions();
            graphicsQualityDropdownUI.AddOptions(new List<string>() {"Low" , "Medium", "High"});
            graphicsQualityDropdownUI.value = PlayerPrefs.HasKey("graphicsQualityIndex") ? PlayerPrefs.GetInt("graphicsQualityIndex") : 2; // ie high on start
            graphicsQualityDropdownUI.RefreshShownValue();
        }
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

    // TODO: work for OS vs Windows
    public void SetResolution(int index) => Screen.SetResolution(resolutionOptions[index].width, resolutionOptions[index].height, Screen.fullScreenMode);

    public void SetFullScreen(bool isFullScreen) => Screen.fullScreen = isFullScreen;

    public void SetGraphicsQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetFloat("graphicsQualityIndex", index);
    }

    public void OnClickRevertToDefault()
    {
        curPopUp = Instantiate(popUpPrefab, transform);
        curPopUp.DisplayPopUp(RevertToDefault);
    }
    
    // TODO: implement controls page and default caching & reverting
    public void RevertToDefault(bool isConfirmed)
    {
        if (isConfirmed)
        {
            Debug.Log("reverting to default");
        }
        else
        {
            Debug.Log("NOT reverting to default");
        }
    }
}
