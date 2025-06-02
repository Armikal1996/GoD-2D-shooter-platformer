using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 3f;
    public LayerMask enemyLayer;

    private Rigidbody2D rb;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Shoot(Vector2 direction, float speed, float damage)
    {
        this.damage = damage;

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.velocity = direction * speed;

        // Rotate to face movement direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var monster = collision.GetComponent<MonsterHealth>();
            if (monster != null)
                monster.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
