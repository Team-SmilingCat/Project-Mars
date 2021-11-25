using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : ScriptableObject
{
    [Header("Entity Status")] 
    public int currentHealth;
    public int maxHealth;
    public string name;

}
