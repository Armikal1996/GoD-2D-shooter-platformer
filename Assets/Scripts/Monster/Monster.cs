using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Monster : MonoBehaviour
{
    public int damageAmount = 10;
    public float detectionRange = 5f;
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isDead = false;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Attack state
        if (distance <= attackRange)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                rb.velocity = Vector2.zero;
                animator.SetBool("IsAttacking", true);
            }
        }
        // Chase state
        else if (distance <= detectionRange)
        {
            isAttacking = false;
            animator.SetBool("IsAttacking", false);
            MoveTowardPlayer();
        }
        // Idle state
        else
        {
            isAttacking = false;
            rb.velocity = Vector2.zero;
            animator.SetBool("IsAttacking", false);
        }
    }

    void MoveTowardPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        // Flip sprite based on movement direction
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Called by animation event
    public void OnAttackAnimationEnd()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
        isAttacking = false;
    }

    public void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsDead", true);
    }
}