using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public Image[] healthbars;

    private int currentHealth;
    private Animator animator;

    // Event that gets triggered when player dies
    public UnityEvent OnPlayerDeath;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        UpdateHealthBars();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        UpdateHealthBars();
        Debug.Log("player got damaged" + currentHealth);

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
        animator.SetBool("IsDead", true);

        Destroy(gameObject, 3f);

        // Invoke the death event
        OnPlayerDeath.Invoke();
    }
}