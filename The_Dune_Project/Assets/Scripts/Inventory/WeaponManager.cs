using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //place where weapon is
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private WeaponDamage weaponDamage;
    [SerializeField] private GameObject rightHand;

    public void Start()
    {
        //eaponDamage = rightHand.GetComponentInChildren<WeaponDamage>();
    }

    public void LoadWeapon(Weapons weapon)
    {
        weaponController.LoadWeapon(weapon);
        weaponDamage = rightHand.GetComponentInChildren<WeaponDamage>();
    }

    public void weaponDamageOnLoad()
    {
        weaponDamage.OnEnableWeapon();
    }

    public void weapoDamageOnDisable()
    {
        weaponDamage.OnDisableWeapon();
    }
}
