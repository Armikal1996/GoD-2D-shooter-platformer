using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class InventorySaveSystem
{
    private static string saveDirectory = Application.persistentDataPath + "/Inventory/";
    private static string saveFilePath = "inventory.json";

    public static void SaveInventory(List<InventoryItem> items)
    {
        if (!Directory.Exists(saveDirectory))
            Directory.CreateDirectory(saveDirectory);

        var saveData = new InventorySaveData { savedItems = items };
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(Path.Combine(saveDirectory, saveFilePath), json);
        Debug.Log("Inventory saved to " + saveDirectory);
    }

    public static List<InventoryItem> LoadInventory()
    {
        string fullPath = Path.Combine(saveDirectory, saveFilePath);

        if (!File.Exists(fullPath))
        {
            Debug.Log("No inventory save file found.");
            return new List<InventoryItem>();
        }

        string json = File.ReadAllText(fullPath);
        var saveData = JsonUtility.FromJson<InventorySaveData>(json);

        // Load all item assets to create a lookup dictionary
        var allItems = Resources.LoadAll<ItemData>("Items");
        var itemLookup = new Dictionary<string, ItemData>();
        foreach (var item in allItems)
        {
            itemLookup[item.UniqueID] = item;
        }

        // Reconnect items by UniqueID
        foreach (var inventoryItem in saveData.savedItems)
        {
            if (itemLookup.TryGetValue(inventoryItem.data.UniqueID, out var itemData))
            {
                inventoryItem.data = itemData;
            }
            else
            {
                Debug.LogWarning($"Item with ID {inventoryItem.data.UniqueID} not found in Resources!");
            }
        }

        Debug.Log("Inventory loaded successfully.");
        return saveData.savedItems;
    }
}
[Serializable]
public class InventorySaveData
{
    public List<InventoryItem> savedItems = new List<InventoryItem>();
}
