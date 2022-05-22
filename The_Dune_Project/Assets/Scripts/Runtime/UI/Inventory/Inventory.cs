using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    void Awake()
	{
		if (instance != null)
		{
			return;
		}
		instance = this;
	}

    public delegate void OnKeyItemChanged();
    public OnKeyItemChanged onKeyItemChangedCallback;
    public delegate void OnConsumableChanged();
    public OnConsumableChanged onConsumableChangedCallback;

    public List<KeyItem> keyItems = new List<KeyItem>();
    public List<Consumable> consumables = new List<Consumable>();

    public GameObject inventoryUI;
    public InventorySlot[] keyItemSlots; // TODO: Change these to private later. Currently public for easier in-editor debugging.
    public InventorySlot[] consumableSlots;

    void Start(){
        onKeyItemChangedCallback += UpdateKeyItemUI;
        onConsumableChangedCallback += UpdateConsumableUI;
        keyItemSlots = inventoryUI.transform.GetChild(0).transform.GetComponentsInChildren<InventorySlot>();
        consumableSlots = inventoryUI.transform.GetChild(1).transform.GetComponentsInChildren<InventorySlot>();

        // TODO: Remove when done testing.
        /*
        Consumable con = ScriptableObject.CreateInstance<Consumable>();
        con.itemName = "test";
        con.Count = 1;
        Add(con);
        */
    }

    public bool Add(Items item){
        if (item is KeyItem)
        {
            KeyItem existingKeyItem = keyItems.Find(i => i.itemName == item.itemName);
            if (existingKeyItem != null)
            {
                Debug.Log("Cannot pickup: Key item already in inventory.");
                return false;
            }
            keyItems.Add((KeyItem) item);
            if (onKeyItemChangedCallback != null) onKeyItemChangedCallback.Invoke();
            return true;
        }
        if (item is Consumable)
        {
            Consumable consumableItem = (Consumable) item;
            Consumable existingConsumable = consumables.Find(i => i.name == item.itemName);
            if (existingConsumable != null)
            {
                existingConsumable.Count += consumableItem.Count;
                if (existingConsumable.OverCapacity())
                {
                    existingConsumable.Count -= consumableItem.Count;
                    Debug.Log("Cannot pickup: Consumable over capacity.");
                    return false;
                }
                if (onConsumableChangedCallback != null) onConsumableChangedCallback.Invoke();
                return true;
            }
            consumables.Add(consumableItem);
            if (onConsumableChangedCallback != null) onConsumableChangedCallback.Invoke();
            return true;
        }
        Debug.Log("Cannot pickup: Item type not recognized.");
        return false;
    }
    
    // For Consumables only. This and UpdateKeyItemUI should be updated if KeyItems can be lost/broken.
    public bool Remove(Consumable consumable, int amountRemoved)
    {
        Consumable existingConsumable = consumables.Find(c => c.name == consumable.itemName);
        if (existingConsumable){
            if (amountRemoved > existingConsumable.Count)
            {
                Debug.Log("Remove failed: Remove quantity greater than owned quantity.");
                return false;
            }
            existingConsumable.Count -= amountRemoved;
            if (onConsumableChangedCallback != null) onConsumableChangedCallback.Invoke();
            return true;
        }
        else
        {
            Debug.Log("Remove failed: Consumable is not in inventory.");
            return false;
        }
    }

    public void UpdateKeyItemUI(){
        foreach(KeyItem ki in keyItems){
            InventorySlot slot = Array.Find(keyItemSlots, k => k.item != null && k.item.itemName == ki.itemName);
            if (slot == null){
                slot = Array.Find(keyItemSlots, k => k.item == null);
                slot.UpdateSlot(ki);
            }
            else{
                KeyItem keyItem = (KeyItem) slot.item;
                slot.UpdateSlot(ki);
            }
        }
    }

    public void UpdateConsumableUI(){
        foreach(Consumable con in consumables){
            InventorySlot slot = Array.Find(consumableSlots, c => c.item != null && c.item.itemName == con.itemName);
            if (slot == null){
                slot = Array.Find(consumableSlots, c => c.item == null);
                slot.UpdateSlot(con);
            }
            else{
                Consumable consumable = (Consumable) slot.item;
                if (con.Count == 0){
                    consumables.Remove(con);
                    slot.UpdateSlot(null);
                }
                else{
                    slot.UpdateSlot(con);
                }
            }
        }
    }
}
