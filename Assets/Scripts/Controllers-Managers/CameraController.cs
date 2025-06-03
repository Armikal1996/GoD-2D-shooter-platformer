using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    Vector3 velocity = Vector3.zero;
    public float yOffset; // Vertical offset of the camera from the player

    private float initialZ;

    [Header("Axis Limitations")]
    public Vector2 xLimit; // (minX, maxX)
    public Vector2 yLimit; // (minY, maxY)

    void Start()
    {
        initialZ = transform.position.z;
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    void LateUpdate()
    {
        // Apply offset and clamp within limits
        float targetX = Mathf.Clamp(player.position.x, xLimit.x, xLimit.y);
        float targetY = Mathf.Clamp(player.position.y + yOffset, yLimit.x, yLimit.y);
        float targetZ = initialZ;

        Vector3 targetPosition = new Vector3(targetX, targetY, targetZ);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
    }
}
