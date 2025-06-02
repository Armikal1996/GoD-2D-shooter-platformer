using UnityEngine;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public GameObject itemDropPrefab;
    public Image healthBar;

    private float currentHealth;
    private Monster monster;

    void Start()
    {
        currentHealth = maxHealth;
        monster = GetComponent<Monster>();
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.fillAmount = currentHealth / maxHealth;
    }

    void Die()
    {
        monster.Die();

        if (itemDropPrefab != null)
            Instantiate(itemDropPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject, 0.5f); // Allow animation to settle
    }
}
