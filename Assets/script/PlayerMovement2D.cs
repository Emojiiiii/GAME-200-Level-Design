using UnityEngine;//玩家移动脚本，放在玩家上

public class PlayerMovement2D : MonoBehaviour
{
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float groundRadius = 0.15f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;

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

        // 地面检测
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // 空格跳跃
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
<<<<<<< HEAD
            rb.velocity = new Vector2(rb.velocity.x, 0f); // 防止叠加导致跳太高/不稳定
=======
            rb.velocity = new Vector2(rb.velocity.x, 0f);
>>>>>>> 7cbd497a6124cd5dc7aa10f5dcf6916b834d452e
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

<<<<<<< HEAD
    // 可选：在Scene里看到检测圈
=======
    void LateUpdate()
    {
        if (shouldDetach)
        {
            shouldDetach = false;

            if (transform.parent != null)
            {
                transform.SetParent(null);
            }

            currentPlatform = null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rewindable>() != null)
        {
            currentPlatform = collision.transform;
            transform.SetParent(currentPlatform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform == currentPlatform)
        {
            shouldDetach = true;
        }
    }

>>>>>>> 7cbd497a6124cd5dc7aa10f5dcf6916b834d452e
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}