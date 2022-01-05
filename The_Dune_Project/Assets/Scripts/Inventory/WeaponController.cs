using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform parentOverride;
    public GameObject weaponMesh;

    public void LoadWeapon(Weapons weapon)
    {
        DestroyOldWeapon();
        if (weapon == null)
        {
            unloadWeapon();
            return;
        }
        
        //unload and destroy to load new weapon
        GameObject newWeapon = Instantiate(weapon.weaponMesh) as GameObject;
        
        if (newWeapon != null)
        {
            if(parentOverride != null)
            {
                newWeapon.transform.parent = parentOverride.transform;   
            }
            else
            {
                newWeapon.transform.parent = gameObject.transform;
            }
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.rotation = Quaternion.identity;
            newWeapon.transform.localScale = Vector3.one; 
        }

        weaponMesh = newWeapon;

    }

    public void unloadWeapon()
    {
        if (weaponMesh != null)
        {
            weaponMesh.SetActive(false);
        }
    }

    public void DestroyOldWeapon()
    {
        if (weaponMesh != null)
        {
            Destroy(weaponMesh);
        }
    }
    
}
