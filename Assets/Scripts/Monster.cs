using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public float detectionRange = 5f;
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;

    public Transform player;
    public GameObject itemDropPrefab;
    public Image healthBar; // Assign in inspector (UI child)

    private Rigidbody2D rb;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        UpdateHealthBar();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            Vector2 dir = (player.position - transform.position).normalized;

            if (distance > attackRange)
                rb.MovePosition(rb.position + dir * moveSpeed * Time.deltaTime);
            else
                Attack();
        }
    }
    void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    void Attack()
    {
        // Placeholder for animation or damage
        Debug.Log("Monster attacks!");
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        UpdateHealthBar();

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Monster died.");
        Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
