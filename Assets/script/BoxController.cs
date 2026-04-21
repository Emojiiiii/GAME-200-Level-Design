using UnityEngine;
using Cinemachine;

public class BoxController : MonoBehaviour
{
    private bool isPlayerInside = false;
    public bool possessed = false;
    private float moveInputX;
    private Rigidbody2D rb2;
    public GameObject ghostPlayer;
    private Rigidbody2D rb;
    public CinemachineVirtualCamera vcam;
    public Transform newTarget;
    public Transform ghostCam;
    public Transform boxCam;
    
    
    void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
        ghostPlayer = GameObject.Find("ghost_white");
        rb = ghostPlayer.GetComponent<Rigidbody2D>();
        ghostCam = ghostPlayer.transform;
        boxCam = this.transform;
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
                Invoke("possessBox", 0.1f);
            }
        }
        if (possessed)
        {
            moveInputX = Input.GetAxisRaw("Horizontal");
            rb2.velocity = new Vector2(moveInputX * 2f, 0f);
        }
    }
    void possessBox()
    {
        if (CheckInteraction())
        {
            possessed = true;
            rb2.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            rb.position = new Vector2(-3.04f, -21.89f);
            newTarget = this.transform;
            newTarget = boxCam;
            Debug.Log(newTarget);
            vcam.Follow = newTarget;
        }
    }

    

    public bool CheckInteraction()
    {
        return isPlayerInside;
    }
}
