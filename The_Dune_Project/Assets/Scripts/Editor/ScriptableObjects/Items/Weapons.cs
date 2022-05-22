using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Items/weapons")]
public class Weapons : Items
{
   public bool isInCombat;
   public string type;
   public int damage;
   public GameObject weaponMesh;

   public int handsUsed;

}
