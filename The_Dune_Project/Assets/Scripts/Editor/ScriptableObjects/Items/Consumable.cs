using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Create Items/Consumable")]
public class Consumable : Items
{
    public int Count { get; set; }
    public int MaxCount { get; }
    public int Target { get; }
    public int Effect { get; }

    public delegate void OnConsumableUsed();
    public OnConsumableUsed onConsumableUsedCallback;

    public Consumable(string name, string? description, Sprite icon, int count, int? maxCount, int target, int effect)
    {
        itemName = name;
        Description = description;
        Icon = icon;
        Count = count;
        MaxCount = maxCount == null ? 99 : maxCount.Value;
        Target = target;
        Effect = effect;
    }

    public void Use(){
        if (onConsumableUsedCallback != null){
            onConsumableUsedCallback.Invoke(); // TODO: Effect of using consumable. (Change stats: e.g. health, speed)
        }
    }

    public bool OverCapacity(){
        if (Count > MaxCount) 
        {
            return true;
        }
        return false;
    }
}
