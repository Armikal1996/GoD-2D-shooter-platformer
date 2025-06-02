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
    private bool isInRange = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        InvokeRepeating(nameof(CheckDistanceToPlayer), 0f, 0.2f);
    }

    void FixedUpdate()
    {
        if (!isDead && !isAttacking && isInRange && player != null)
        {
            MoveTowardPlayer();
        }
    }

    void CheckDistanceToPlayer()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                isInRange = true;
                rb.velocity = Vector2.zero;
                animator.SetBool("IsAttacking", true); // Trigger animation
            }
        }
        else if (distance <= detectionRange)
        {
            isInRange = false;
            animator.SetBool("IsAttacking", false);
        }
        else
        {
            // Player out of detection range
            isInRange = false;
            isAttacking = false;
            animator.SetBool("IsAttacking", false);
        }
    }

    void MoveTowardPlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
    }

    // This function is called by an animation event
    public void OnAttackAnimationEnd()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("Monster damaged the player!");
            }
        }

        isAttacking = false;
        animator.SetBool("IsAttacking", false);
    }

    public void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("IsAttacking", false);
    }
}
