using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    public ItemData testItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (testItem != null && InventoryManager.Instance != null)
            {
                InventoryManager.Instance.AddItem(testItem);
                Debug.Log($"Added item: {testItem.itemName}");
            }
        }
    }
}
