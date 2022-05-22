using System;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

// Abstract pop up, use default prefab for consistent style between all pop ups in game
public class PopUp : MonoBehaviour
{
    [SerializeField] private GameObject popUpObj;
    [SerializeField] private Image hiddenOverlayPanel;
    [SerializeField] private TextMeshProUGUI popUpText;
    [SerializeField] private Button popUpYesButton;
    [SerializeField] private Button popUpNoButton;
    private Action<bool> callback;

    private void Awake()
    {
        popUpObj.SetActive(false);
        hiddenOverlayPanel.enabled = false;
    }

    public bool IsPopUpActive()
    {
        return (hiddenOverlayPanel.enabled || popUpObj.activeInHierarchy);
    }
    
    public void DisplayPopUp(Action<bool> newCallback, string message = "Are you sure?", string confirmButtonText = "Yes", string denyButtonText = "No")
    {
        popUpObj.SetActive(true);
        hiddenOverlayPanel.enabled = true;
        popUpText.text = message;
        
        var yesText = popUpYesButton.GetComponentInChildren<TextMeshProUGUI>();
        yesText.text = yesText ? confirmButtonText : "Yes";

        var noText = popUpNoButton.GetComponentInChildren<TextMeshProUGUI>();
        noText.text = noText ? denyButtonText : "No";
        
        popUpYesButton.onClick.AddListener(delegate { OnClickPopUpButton(true); } );
        popUpNoButton.onClick.AddListener(delegate { OnClickPopUpButton(false); } );
        
        this.callback = newCallback;
    }

    private void ClosePopUp()
    {
        popUpObj.SetActive(false);
        hiddenOverlayPanel.enabled = false;
        
        popUpYesButton.onClick.RemoveAllListeners();
        popUpNoButton.onClick.RemoveAllListeners();
    }

    public void OnClickPopUpButton(bool isConfirming)
    {
        callback(isConfirming);
        ClosePopUp();
    }
}
