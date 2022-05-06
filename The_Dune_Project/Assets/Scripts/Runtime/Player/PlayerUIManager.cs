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
    [SerializeField] private float inventoryCD;
    [SerializeField] private float closeCD;

    [Header("Flag")] 
    [SerializeField] private bool inventoryIsOpen;
    public bool canOpenInventory;
    public bool canCloseInventory;


    private void Awake()
    {
        animatorManager = player.GetComponent<AnimatorManager>();
        playerManager = player.GetComponent<PlayerManager>();
        inputHandle = player.GetComponent<PlayerInputHandle>();
    }

    private void Start()
    {
        InitHealthBar();
        canOpenInventory = true;
        inventoryIsOpen = false;
    }

    private void Update()
    {

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

    public void CheckForInventoryInput()
    {
        if (!inventoryIsOpen && inputHandle.inventoryInput)
        {
            //inputHandle.HandleInventoryInput();
            SetInventoryWindow();
            playerManager.SwitchToInventoryState();
        }
    }

    public void CheckForInventoryClosingInput()
    {
        if (inventoryIsOpen && inputHandle.inventoryInput && canOpenInventory)
        {
            RemoveInventoryWindow();
            StartCoroutine(EnsureInventoryClosing());
        }
    }

    private void SetInventoryWindow()
    {
        //enable inventory if it is not already
        if (!inventory.activeSelf 
        && canOpenInventory)
        {
            Debug.Log("inventory has opened");
            //TODO let cameraManager take care of camera priorities
            invCam.Priority = 10;
            inventoryIsOpen = true;
            animatorManager.PlayTargetAnimation("openbag", true);
            inventory.SetActive(true);
            StartCoroutine(CheckInventoryTimer());
        }
    }

    private void RemoveInventoryWindow()
    {
        Debug.Log("can remove");
        if (canOpenInventory && inventoryIsOpen)
        {
            invCam.Priority = 1;
            inventoryIsOpen = false;
            //change this
            animatorManager.PlayTargetAnimation("openbag", true);
            Debug.Log("closed inv");
            inventory.SetActive(false);
        }
        
    }



    private IEnumerator CheckInventoryTimer()
    {
        canOpenInventory = false;
        yield return new WaitForSeconds(inventoryCD);
        canOpenInventory = true;
    }

    private IEnumerator EnsureInventoryClosing()
    {
        yield return new WaitForSeconds(closeCD);
        //player can move only after cd seconds, this is to ensure the inventory is closed before hand.
        playerManager.SwitchToActiveState();
    }
    
}
