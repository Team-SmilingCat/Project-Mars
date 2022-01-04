using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

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
    
}
