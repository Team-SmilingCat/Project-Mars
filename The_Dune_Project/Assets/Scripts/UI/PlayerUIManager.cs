using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private List<Sprite> crossHairList; 

    [SerializeField] private GameObject uiCrossHairUsed;

    private void Start()
    {
        if (slider.value == 0)
        {
            EntityStats stats = GetComponent<EntityStats>();
            if (stats)
            {
                SetMaxHealth(stats.maxHealth);
            }
        }
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
    
    public void SetHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }

    public void DisableCrossHair()
    {
        uiCrossHairUsed.gameObject.SetActive(false);
    }

    public void ChangeCrossHair(Sprite crosshair)
    {
        if(!uiCrossHairUsed.activeSelf){
            uiCrossHairUsed.SetActive(true);
        }
        foreach(Sprite i in crossHairList){
            if(i.Equals(crosshair)){
                uiCrossHairUsed.GetComponent<Image>().sprite = i;
            }
        }
    }
    
}
