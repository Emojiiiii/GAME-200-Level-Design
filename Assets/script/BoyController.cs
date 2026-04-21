using UnityEngine;
using Cinemachine;

public class BoyController : MonoBehaviour
{
    private bool isPlayerInside = false;
    public bool possessed = false;
    private float moveInputX;
    private Rigidbody2D rb2;
    GameObject ghostPlayer;
    private Rigidbody2D rb;
    public CinemachineVirtualCamera vcam;
    public Transform newTarget;
    public Transform ghostCam;
    public Transform boyCam;

    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float groundRadius = 0.15f;
    public LayerMask groundLayer;

    private float moveInput;
    private bool isGrounded;

    private Transform currentPlatform;
    
    
    void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
        ghostPlayer = GameObject.Find("ghost_white");
        rb = ghostPlayer.GetComponent<Rigidbody2D>();
        ghostCam = ghostPlayer.transform;
        boyCam = this.transform;
    }

    // Detect when player enters the box
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    // Detect when player leaves the box
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    void Update()
    {
        // Check for button press (e.g., the 'E' key)
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (possessed)
            {
                possessed = false;
                rb2.constraints |= RigidbodyConstraints2D.FreezePositionX;
                rb.position = new Vector2(rb2.position.x + (moveInputX * 0.25f), rb2.position.y);
                newTarget = ghostCam;
                Debug.Log(newTarget);
                vcam.Follow = newTarget;
            }
            else if (CheckInteraction())
            {
                possessed = true;
                rb2.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                rb.position = new Vector2(-3.04f, -21.89f);
                newTarget = this.transform;
                newTarget = boyCam;
                Debug.Log(newTarget);
                vcam.Follow = newTarget;
            }
        }
        if (possessed)
        {
            moveInput = Input.GetAxisRaw("Horizontal");

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb2.velocity = new Vector2(rb2.velocity.x, 0f);
                rb2.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                
            }
            
        }
    }

    void FixedUpdate()
    {
        if (possessed)
        {
            rb2.velocity = new Vector2(moveInput * moveSpeed, rb2.velocity.y);
        }
    }
    

    public bool CheckInteraction()
    {
        return isPlayerInside;
    }
}
