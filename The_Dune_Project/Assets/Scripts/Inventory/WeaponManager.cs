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
    [SerializeField] private Animator animator;

    public void Start()
    {

    }

    public void LoadWeapon(Weapons weapon)
    {
        weaponController.LoadWeapon(weapon);
        weaponDamage = rightHand.GetComponentInChildren<WeaponDamage>();
        if (weapon.type.Equals("ranged"))
        {
            animator.SetLayerWeight(1, 1);
        }
    }

    public void weaponDamageOnLoad()
    {
        weaponDamage.OnEnableWeapon();
    }

    public void weaponDamageOnDisable()
    {
        weaponDamage.OnDisableWeapon();
    }
}
