using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    public WeaponData weaponToGive;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var weaponHandler = other.GetComponent<PlayerWeaponHandler>();
            if (weaponHandler.currentWeapon.weaponTypeID < weaponToGive.weaponTypeID) // equip better weapon immediately
            {
                weaponHandler.EquipWeapon(weaponToGive);
                Destroy(gameObject); 
            }
            else
            {
                FindObjectOfType<InventoryManager>().AddItem(weaponToGive);
                Destroy(gameObject);
            }
        }
    }
}
