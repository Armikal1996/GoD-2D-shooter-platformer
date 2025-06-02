using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI quantityText;
    public Button deleteButton;

    private InventoryItem currentItem;

    public System.Action<InventorySlot> onDeleteRequest;

    public void SetItem(InventoryItem item)
    {
        currentItem = item;
        iconImage.sprite = item.data.icon;
        iconImage.enabled = true;

        if (item.data.isStackable && item.quantity > 1)
            quantityText.text = item.quantity.ToString();
        else
            quantityText.text = "";

        deleteButton.gameObject.SetActive(false);
    }

    public void ClearSlot()
    {
        currentItem = null;
        iconImage.sprite = null;
        iconImage.enabled = false;
        quantityText.text = "";
        deleteButton.gameObject.SetActive(false);
    }

    public void OnSlotClicked()
    {
        deleteButton.gameObject.SetActive(currentItem != null);
    }

    public void OnDeleteClicked()
    {
        onDeleteRequest?.Invoke(this);
    }

    public InventoryItem GetItem() => currentItem;
}
