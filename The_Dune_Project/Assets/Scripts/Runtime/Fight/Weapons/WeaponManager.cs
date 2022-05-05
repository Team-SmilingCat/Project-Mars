using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //place where weapon is
    [SerializeField] private GameObject rightHand;
    [SerializeField] private Animator animator;

    [Header("player components")]
    [SerializeField] private WeaponController weaponController;
    //weapon damage is loaded in runtime
    [SerializeField] private WeaponDamage weaponDamage;


    [Header("moving player during combo properties")]
    [SerializeField] private float moveAmount;

    private void Awake()
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
        weaponDamage.OnEnableWeaponCollider();
    }

    public void weaponDamageOnDisable()
    {
        weaponDamage.OnDisableWeaponCollider();
    }


}
