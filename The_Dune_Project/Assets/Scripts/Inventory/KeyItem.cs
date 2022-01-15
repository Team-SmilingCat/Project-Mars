using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KeyItem", menuName = "Create Items/KeyItem")]
public class KeyItem : Items
{
    public delegate void OnKeyItemUsed();
    public OnKeyItemUsed onKeyItemUsedCallback;

    public KeyItem(string name, Sprite icon)
    {
        itemName = name;
        Icon = icon;
    }

    public void Use(){
        if (onKeyItemUsedCallback != null){
            onKeyItemUsedCallback.Invoke(); //TODO: Effect of using key item. (e.g. unlock door)
        }
    }
}
