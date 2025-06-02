using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    public static PlayerWeaponHandler Instance;
    public WeaponData currentWeapon;
    public Animator animator;


    void Start()
    {
        Instance = this;
        EquipWeapon(currentWeapon);
        animator = GetComponent<Animator>();
    }

    public void EquipWeapon(WeaponData weapon)
    {
        currentWeapon = weapon;

        if (animator != null)
        {
            animator.SetInteger("WeaponType", weapon.weaponTypeID);
        }
    }
}
