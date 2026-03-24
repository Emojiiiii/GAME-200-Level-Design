using UnityEngine;//敌人追逐脚本，放在敌人上

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 4f;
    public bool canChase = true;

    [Header("Flashlight Reaction")]
    public bool hitByFlashlight = false;
    public bool avoidFlashlight = true;   // true=躲避，false=停止
    public float avoidSpeedMultiplier = 1.2f;

    private Rigidbody2D rb;

    void Start()
    {
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (hitByFlashlight)
        {
            if (avoidFlashlight)
            {
                AvoidPlayer();
            }
            else
            {
                rb.velocity = Vector2.zero;
            }

            return;
        }

        if (!canChase)
        {
            rb.velocity = Vector2.zero;
            // Debug.Log("Enemy chase blocked");
            return;
        }

        ChasePlayer();
    }

    void ChasePlayer()
    {
        // Debug.Log("Enemy chase running");

        float dir = Mathf.Sign(player.position.x - transform.position.x);
        rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);

        Flip(dir);
    }

    void AvoidPlayer()
    {
        float dirAway = Mathf.Sign(transform.position.x - player.position.x);
        rb.velocity = new Vector2(dirAway * moveSpeed * avoidSpeedMultiplier, rb.velocity.y);

        Flip(dirAway);
    }

    void Flip(float dir)
    {
        if (dir != 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x) * -dir,
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    public void SetFlashlightHit(bool value)
    {
        hitByFlashlight = value;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Caught!");
            // 在这里写失败逻辑，比如重开关卡
        }
    }
}