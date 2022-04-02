using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject player;
    
    [Header("Inventory")]
    [SerializeField] private GameObject inventory;
    private AnimatorManager animatorManager;
    private PlayerManager playerManager;
    private PlayerInputHandle inputHandle;
    [SerializeField] private CinemachineVirtualCamera invCam;

    private void Awake()
    {
        animatorManager = player.GetComponent<AnimatorManager>();
        playerManager = player.GetComponent<PlayerManager>();
        inputHandle = player.GetComponent<PlayerInputHandle>();
    }

    private void Start()
    {
        InitHealthBar();
    }

    private void Update()
    {
        EnableInventory();
    }

    private void InitHealthBar()
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

    public void EnableInventory()
    {
        //enable inventory if it is not already
        if (!inventory.activeSelf && inputHandle.inventoryInput)
        {
            //TODO let cameraManager take care of camera priorities
            invCam.Priority = 10;
            animatorManager.PlayTargetAnimation("openbag", true);
            inventory.SetActive(true);
        }
        else
        {
            invCam.Priority = 1;
        }
        
        
    }


}
