using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Start()
    {
        Debug.Log(gameObject.name + " canChase at Start = " + canChase);
    }
     public Transform player;
    public float moveSpeed = 4f;
    public bool canChase = true; 

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // 追逐战一般不需要重力（需要重力就删掉这行）
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (!canChase || player == null)
        {
            rb.velocity = Vector2.zero;
            Debug.Log("Enemy chase blocked");
            return;
        }

        Debug.Log("Enemy chase running");

        float dir = Mathf.Sign(player.position.x - transform.position.x);
        rb.velocity = new Vector2(dir * moveSpeed, 0f);

        if (dir != 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x) * -dir,//翻面
                transform.localScale.y,
                transform.localScale.z
            );
        }
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
