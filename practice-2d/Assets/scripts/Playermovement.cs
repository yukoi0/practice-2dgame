using UnityEngine;

public class Playermoveme : MonoBehaviour
{
    private Rigidbody2D body;
    private CircleCollider2D boxCollider;
    private bool canDoubleJump = false;
    private bool isGrounded = false;

    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        // Movement
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }
        print(OnWall());
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            canDoubleJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = IsGroundedCheck();
    }

    private bool IsGroundedCheck()
    {
        float circleRadius = boxCollider.radius;
        RaycastHit2D raycastHit = Physics2D.CircleCast(boxCollider.bounds.center, circleRadius, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool OnWall()
    {
        float circleRadius = boxCollider.radius;
        RaycastHit2D raycastHit = Physics2D.CircleCast(boxCollider.bounds.center, circleRadius, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
