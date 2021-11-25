using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    
    public Weapons weapon;

    private void Start()
    {
        weaponManager.LoadWeapon(weapon);
    }
}
