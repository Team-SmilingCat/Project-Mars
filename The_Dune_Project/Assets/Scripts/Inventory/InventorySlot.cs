using UnityEngine;
using UnityEngine.UI;

public class InventorySlot
{
    public Image icon;
    public int? count;
    public Color slotColour = Color.gray; // TODO: Change this if an item slot should be highlighted in some way.
    public Items item;

    public void UpdateSlot(Items? newItem){
        if (newItem == null){
            icon.sprite = null;
            item = null;
        }
        else {
            if (newItem is Consumable) count = ((Consumable)newItem).Count;
            icon.sprite = newItem.Icon;
            item = newItem;
        }
    }
}
