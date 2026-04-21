using UnityEngine;//玩家移动脚本，放在玩家上

public class TomPlayerMovement : MonoBehaviour
{
    private GameObject ghost;
    public float moveSpeed = 5f;
    [Header("Jump")]
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float groundRadius = 0.15f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private BoxCollider2D cld;
    private float moveInputX;
    private float moveInputY;
    private bool isGrounded;

    private Transform currentPlatform;
    private bool shouldDetach = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cld = GetComponent<BoxCollider2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");

        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        
    }

    void FixedUpdate()
    {
        ghost = gameObject;
        GameObject duplicateX = Instantiate(ghost);
        /*duplicateX.rb = rb;
        duplicateX.cld = cld;
        duplicateX.rb.position = rb.position;
        duplicateX.rb.velocity.x = moveInputX * moveSpeed;
        if (duplicateX.rb.IsTouchingLayers("ground"))
        {
            moveInputX = 0;
        }*/
        Destroy(duplicateX);

        rb.velocity = new Vector2(moveInputX * moveSpeed, moveInputY * moveSpeed);
    }

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

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}