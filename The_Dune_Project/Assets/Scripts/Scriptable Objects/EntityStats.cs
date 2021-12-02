using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Stats/entity stats")]
public class EntityStats : ScriptableObject
{
    [Header("Entity Status")] 
    public int currentHealth;
    public int maxHealth;
    public int damage;
    public string name;
    public int moveSpeed;

}
