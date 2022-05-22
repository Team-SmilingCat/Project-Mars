using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : ScriptableObject
{
    [Header("item data")] 
    public string itemName;
    public string? Description;
    public Sprite Icon;
}
