using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //place where weapon is
    [SerializeField] private WeaponController weaponController;

    public void LoadWeapon(Weapons weapon)
    {
        weaponController.LoadWeapon(weapon);
        
        
    }
}
