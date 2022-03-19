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
    private CharacterController playerBody;
    [SerializeField] private WeaponController weaponController;
    //weapon damage is loaded in runtime
    [SerializeField] private WeaponDamage weaponDamage;


    [Header("moving player during combo properties")]
    [SerializeField] private float moveAmount;

    private void Awake()
    {
        playerBody = gameObject.GetComponent<CharacterController>();

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

    // TODO: allow movement before second attack button pressed?
    // move (rotate?) in that direction then cause animation? cutting the recovery part of the anim of the first attack
    public void MovePlayerInSequence()
    {
        playerBody.Move(gameObject.transform.forward * moveAmount * Time.deltaTime);

    }
}
