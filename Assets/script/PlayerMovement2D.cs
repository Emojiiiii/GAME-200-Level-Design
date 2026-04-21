using UnityEngine; // 玩家移动脚本，放在玩家上

public class PlayerMovement2D : MonoBehaviour
{
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Slope")]
    public float slopeCheckDistance = 0.4f;
    public float maxSlopeAngle = 50f;

    [Header("Flashlight")]

    // private bool facingRight = true;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private Vector2 groundNormal = Vector2.up;

    private Transform currentPlatform;
    private bool shouldDetach = false;

    void Awake()
    {   
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // Debug.Log("timeScale = " + Time.timeScale);
        // Debug.Log("IsPaused = " + TimePauseManager.IsPaused);
        // Debug.Log("moveInput = " + moveInput);

        CheckGround();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;

        if (isGrounded)
        {
            float slopeAngle = Vector2.Angle(groundNormal, Vector2.up);

            if (slopeAngle > 0.1f && slopeAngle <= maxSlopeAngle)
            {
                Vector2 slopeTangent = new Vector2(groundNormal.y, -groundNormal.x).normalized;

                if (Mathf.Sign(slopeTangent.x) != Mathf.Sign(moveInput) && moveInput != 0)
                {
                    slopeTangent = -slopeTangent;
                }

                velocity = slopeTangent * (moveInput * moveSpeed);
            }
            else
            {
                velocity.x = moveInput * moveSpeed;
            }
        }
        else
        {
            velocity.x = moveInput * moveSpeed;
        }

        rb.velocity = velocity;
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, slopeCheckDistance, groundLayer);

        if (hit)
            groundNormal = hit.normal;
        else
            groundNormal = Vector2.up;
    }

    void LateUpdate()
    {
        transform.localScale = new Vector3(0.3f, 0.2f, 1f);

        if (shouldDetach)
        {
            shouldDetach = false;

            if (transform.parent != null)
            {
                transform.SetParent(null, true);
            }

            currentPlatform = null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rewindable>() != null)
        {
            currentPlatform = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform == currentPlatform)
        {
            shouldDetach = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * slopeCheckDistance);
    }
}