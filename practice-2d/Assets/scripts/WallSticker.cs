using UnityEngine;

public class WallSticker : MonoBehaviour
{
    public float wallCheckDistance = 0.1f;
    public LayerMask wallLayer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Check if the player is next to a wall
        bool isNextToWall = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, wallLayer) ||
                             Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, wallLayer);

        // Apply reduced gravity if the player is next to a wall
        if (isNextToWall)
        {
            // Turn off gravity
            rb.gravityScale = 0f;
        }
        else
        {
            // Restore default gravity
            rb.gravityScale = 1f;
        }
    }
}
