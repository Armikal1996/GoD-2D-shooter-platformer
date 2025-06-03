using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string uniqueID; // Made private with getter
    public string itemName;
    public Sprite icon;
    public bool isStackable;
    public string description;

    public string UniqueID
    {
        get
        {
            // Generate new ID if empty
            if (string.IsNullOrEmpty(uniqueID))
                uniqueID = System.Guid.NewGuid().ToString();
            return uniqueID;
        }
    }

    // Reset ID when creating new item
    private void OnValidate()
    {
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(uniqueID))
            uniqueID = System.Guid.NewGuid().ToString();
#endif
    }
}