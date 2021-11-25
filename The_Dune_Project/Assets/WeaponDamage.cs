using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    private Collider weaponHitBox;

    private void Awake()
    {
        weaponHitBox = gameObject.GetComponent<Collider>();
        weaponHitBox.gameObject.SetActive(true);
        weaponHitBox.isTrigger = true;
    }

    public void OnEnableWeapon()
    {
        weaponHitBox.enabled = true;
    }

    public void OnDisableWeapon()
    {
        weaponHitBox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(""))
        {
            //take damage
            //check enemy hp using scritable object
        }
    }
}
