using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Items/weapons")]
public class Weapons : Items
{
   public bool isInCombat;
   public int damage;
   public GameObject weaponMesh;

   public string atk1;
   public string atk2;
   public string atk3;
}
