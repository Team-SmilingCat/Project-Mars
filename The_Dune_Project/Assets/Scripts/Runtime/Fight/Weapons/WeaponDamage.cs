using System;
using System.Collections;
using System.Collections.Generic;
using Fight;
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

    public void OnEnableWeaponCollider()
    {
        gameObject.GetComponent<Collider>().enabled = true;
    }

    public void OnDisableWeaponCollider()
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
            var mobStats = other.GetComponent<Fighter>();
            mobStats.TakeDamage(10);
        }
    }
}
