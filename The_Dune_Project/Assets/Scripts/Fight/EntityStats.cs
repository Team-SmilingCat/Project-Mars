using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Stats/entity stats")]
public abstract class EntityStats : ScriptableObject
{
    [Header("Entity Status")] 
    public string name;
    public int maxHealth;
}
