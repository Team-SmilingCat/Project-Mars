using UnityEngine;
using UnityEngine.UI;

public class InventorySlot: MonoBehaviour
{
    public Image icon;
    public Sprite defaultIcon;
    public int count = 1; // Default value of 1.
    public Color slotColour = Color.gray; // TODO: Change this if an item slot should be highlighted in some way.
    public Items item;

    public void Start(){
        icon = gameObject.GetComponent<Image>();
        icon.color = slotColour;
    }

    public void UpdateSlot(Items? newItem){
        if (newItem == null){
            icon.sprite = null;
            item = null;
            slotColour = Color.gray;
            icon.color = slotColour;
        }
        else {
            if (newItem is Consumable) count = ((Consumable)newItem).Count;
            icon.sprite = newItem.Icon == null ? defaultIcon:newItem.Icon;
            item = newItem;
            slotColour = Color.white;
            icon.color = slotColour;
        }
    }
}
