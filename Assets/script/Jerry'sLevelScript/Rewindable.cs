using UnityEngine;// 可回溯组件，放在需要被回溯的物体上，按 E 键开始回溯，自动返回初始位置和旋转，并保持乘客（玩家）相对位置不变

public class Rewindable : MonoBehaviour
{
    [Header("Return Settings")]
    public float returnSpeed = 8f;
    public float rotationSpeed = 360f;
    public float stopDistance = 0.05f;
    public float stopAngle = 1f;

    [Header("Layer")]
    public string normalLayerName = "Pipe";
    public string rewindLayerName = "PipeRewinding";

    private Rigidbody2D rb;
    private RigidbodyType2D originalBodyType;

    private Vector2 initialPosition;
    private float initialRotation;

    private bool isReturning = false;

    private Rigidbody2D passengerRb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalBodyType = rb.bodyType;

        initialPosition = rb.position;
        initialRotation = rb.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isReturning)
        {
            StartReturn();
        }
    }

    void FixedUpdate()
    {
        if (isReturning)
        {
            ReturnToInitialStateSmooth();
        }
    }

    void StartReturn()
    {
        isReturning = true;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        gameObject.layer = LayerMask.NameToLayer(rewindLayerName);
    }

    void ReturnToInitialStateSmooth()
    {
        Vector2 oldPosition = rb.position;

        Vector2 newPosition = Vector2.MoveTowards(
            rb.position,
            initialPosition,
            returnSpeed * Time.fixedDeltaTime
        );

        float newRotation = Mathf.MoveTowardsAngle(
            rb.rotation,
            initialRotation,
            rotationSpeed * Time.fixedDeltaTime
        );

        Vector2 delta = newPosition - oldPosition;

        rb.MovePosition(newPosition);
        rb.MoveRotation(newRotation);

        if (passengerRb != null)
        {
            passengerRb.position += delta;
        }

        bool reachedPosition = Vector2.Distance(newPosition, initialPosition) <= stopDistance;
        bool reachedRotation = Mathf.Abs(Mathf.DeltaAngle(newRotation, initialRotation)) <= stopAngle;

        if (reachedPosition && reachedRotation)
        {
            rb.position = initialPosition;
            rb.rotation = initialRotation;

            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = originalBodyType;

            gameObject.layer = LayerMask.NameToLayer(normalLayerName);

            isReturning = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y < -0.5f)
            {
                passengerRb = collision.rigidbody;
                return;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.rigidbody == passengerRb)
        {
            passengerRb = null;
        }
    }
}