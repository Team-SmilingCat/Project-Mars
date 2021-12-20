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
        gameObject.GetComponent<Collider>().enabled = true;
    }

    public void OnDisableWeapon()
    { 
        gameObject.GetComponent<Collider>().enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Mob"))
        {
            //take damage
            //check enemy hp using scritable object
            Debug.Log("weapon is colliding with enemy");
            var mobStats = other.GetComponent<EntityStatsManager>();
            mobStats.TakeDamage(10);
        }
    }
}
