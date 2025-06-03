using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{
    public LayerMask enemyLayers;
    public Joystick joystick;
    private PlayerWeaponHandler weaponHandler;
    private Vector2 shootDir = Vector2.right; //defaukt

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
    }

    public void Attack()
    {
        weaponHandler = PlayerWeaponHandler.Instance;
        joystick = FindObjectOfType<FixedJoystick>();

        if (weaponHandler == null)
        {
            return;
        }

        if (weaponHandler.currentWeapon == null)
        {
            return;
        }

        int weaponType = weaponHandler.currentWeapon.weaponTypeID;

        if (weaponType == 0)
            DoMeleeAttack();
        else
            ShootProjectile();
    }

    void DoMeleeAttack()
    {
        float attackRange = weaponHandler.currentWeapon.range;
        int attackDamage = weaponHandler.currentWeapon.damage;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            var monster = enemy.GetComponent<MonsterHealth>();
            if (monster != null)
            {
                monster.TakeDamage(attackDamage);
            }
        }
    }

    void ShootProjectile()
    {
        Vector3 spawnPos = FindFirstObjectByType<PlayerCombat>().transform.position;
        WeaponData weapon = weaponHandler.currentWeapon;

        shootDir = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;

        if (weapon.projectilePrefab == null || shootDir == Vector2.zero)
            return;

        GameObject bullet = Instantiate(weapon.projectilePrefab, spawnPos, Quaternion.identity);

        Projectile proj = bullet.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.Shoot(shootDir, weapon.projectileSpeed, weapon.damage);
        }
    }


    void OnDrawGizmosSelected()
    {
        if (weaponHandler != null && weaponHandler.currentWeapon != null && weaponHandler.currentWeapon.weaponTypeID == 0)
        {
            Gizmos.DrawWireSphere(transform.position, weaponHandler.currentWeapon.range);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position, 1.5f);
        }
    }
}
