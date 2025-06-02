using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<InventorySlot> slots;

    private List<InventoryItem> inventoryItems = new List<InventoryItem>();
    private void Awake()
    {
        Instance = this;
    }
    public void AddItem(ItemData data)
    {
        // Check if item is stackable and already exists
        if (data.isStackable)
        {
            foreach (var item in inventoryItems)
            {
                if (item.data == data)
                {
                    item.quantity++;
                    RefreshUI();
                    return;
                }
            }
        }

        // Add new item if not stackable or doesn't exist yet
        if (inventoryItems.Count < slots.Count)
        {
            inventoryItems.Add(new InventoryItem(data));
            RefreshUI();
        }
        else
        {
            Debug.Log("Inventory full!");
        }
    }

    public void RemoveItemAtSlot(InventorySlot slot)
    {
        var item = slot.GetItem();
        if (item != null)
        {
            inventoryItems.Remove(item);
            RefreshUI();
        }
    }

    private void RefreshUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < inventoryItems.Count)
            {
                slots[i].SetItem(inventoryItems[i]);
                slots[i].onDeleteRequest = RemoveItemAtSlot;
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
