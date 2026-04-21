using UnityEngine;

public class Level4SpawnFix : MonoBehaviour
{
    public Vector2 safeStartPosition = new Vector2(0f, 0f);
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (rb != null)
        {
            rb.position = safeStartPosition;
            rb.velocity = Vector2.zero;
        }
        else
        {
            transform.position = safeStartPosition;
        }
    }
}