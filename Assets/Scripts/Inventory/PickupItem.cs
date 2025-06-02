using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemData itemData;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<InventoryManager>().AddItem(itemData);
            Destroy(gameObject);
        }
    }
}
