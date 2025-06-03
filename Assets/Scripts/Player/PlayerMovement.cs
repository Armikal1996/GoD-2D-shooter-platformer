using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Joystick joystick;
    private Rigidbody2D rb;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joystick = FindObjectOfType<FixedJoystick>();
    }

    void Update()
    {
        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);
        rb.velocity = direction * moveSpeed;

        // Flip character based on movement direction
        if (direction.x > 0 && !isFacingRight)
        {
            FlipCharacter();
        }
        else if (direction.x < 0 && isFacingRight)
        {
            FlipCharacter();
        }
    }
    void FlipCharacter()
    {
        // Switch the way the player is facing
        isFacingRight = !isFacingRight;

        // Multiply the player's x local scale by -1 to flip
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
