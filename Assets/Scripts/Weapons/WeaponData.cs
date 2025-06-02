using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Combat/Weapon")]
public class WeaponData : ItemData
{
    public int damage;
    public float range;
    public GameObject projectilePrefab;
    public float projectileSpeed;

    [Tooltip("0 = No Weapon, 1 = Pistol, 2 = AK-74")]
    public int weaponTypeID;
}
