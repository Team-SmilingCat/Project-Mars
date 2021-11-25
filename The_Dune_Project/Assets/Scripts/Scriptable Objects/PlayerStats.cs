using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Stats/player stats")]
public class PlayerStats : ScriptableObject
{
    [Header("player Stats")] 
    public int currentHealth;
    public int maxHealth;
    public string name;
    public int numBullets;

}
