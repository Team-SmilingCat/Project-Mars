using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private Items item;
    [SerializeField] private Inventory playerInventory;

    private Consumable pickUppableitem;

    // Start is called before the first frame update
    void Start()
    {
        pickUppableitem = new Consumable(item.itemName, item.Description, item.Icon, 1, 99, 1, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory.Add(pickUppableitem);
        }
        gameObject.SetActive(false);
    }
}
