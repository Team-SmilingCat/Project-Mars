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
    InventorySlot[] keyItemSlots;
    InventorySlot[] consumableSlots;
    void Start(){
        onKeyItemChangedCallback += UpdateKeyItemUI;
        onConsumableChangedCallback += UpdateConsumableUI;
        keyItemSlots = inventoryUI.transform.GetChild(0).transform.GetComponentsInChildren<InventorySlot>(); // TODO: Set up UI in Unity.
        consumableSlots = inventoryUI.transform.GetChild(1).transform.GetComponentsInChildren<InventorySlot>();
    }

    public bool Add(Items item){
        if (item is KeyItem)
        {
            KeyItem existingKeyItem = keyItems.Find(i => i.name == item.name);
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
            Consumable existingConsumable = consumables.Find(i => i.name == item.name);
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
    
    public bool Remove(Consumable consumable, int amountRemoved)
    {
        Consumable existingConsumable = consumables.Find(c => c.name == consumable.name);
        if (existingConsumable){
            if (amountRemoved > existingConsumable.Count)
            {
                Debug.Log("Remove failed: Remove quantity greater than owned quantity.");
                return false;
            }
            if (amountRemoved == existingConsumable.Count)
            {
                consumables.Remove(existingConsumable);
                if (onConsumableChangedCallback != null) onConsumableChangedCallback.Invoke();
                return true;
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

    public void UpdateKeyItemUI(){ // TODO: FIX THESE METHODS.
        
    }
    public void UpdateConsumableUI(){
        
    }
}
