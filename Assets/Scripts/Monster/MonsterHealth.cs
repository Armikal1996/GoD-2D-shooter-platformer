using UnityEngine;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    public Image[] healthbars;
    public GameObject itemDropPrefab; // Assign in Inspector

    private float currentHealth;
    private Monster monster;

    void Start()
    {
        currentHealth = maxHealth;
        monster = GetComponent<Monster>();
        UpdateHealthBars();
    }

    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);
        UpdateHealthBars();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBars()
    {
        for (int i = 0; i < healthbars.Length; i++)
            healthbars[i].enabled = (i < currentHealth); // Show only active hearts
    }

    void Die()
    {
        monster.Die();

        Destroy(gameObject, 2f);

        if (itemDropPrefab != null)
            Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
    }
}