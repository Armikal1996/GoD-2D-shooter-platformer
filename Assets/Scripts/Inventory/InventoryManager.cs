using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public GameObject inventoryPanel;

    public List<InventorySlot> slots;
    private List<InventoryItem> inventoryItems = new List<InventoryItem>();

    private void Awake()
    {
        Instance = this;

        inventoryItems = InventorySaveSystem.LoadInventory();
        RefreshUI();
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

    public void ToggleBackpack()
    {
        if (!inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(true);
        }
        else
            inventoryPanel.SetActive(false);

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
    private void OnApplicationQuit()
    {
        InventorySaveSystem.SaveInventory(inventoryItems);
    }
}
