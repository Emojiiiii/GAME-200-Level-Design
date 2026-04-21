using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool platformType;
    private bool movePosition = false;
    private float targetY;
    private bool isMoving = false;
    private float moveProgress;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        moveProgress = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    public void startMoving()
        {
            if (!isMoving)
            {
                isMoving = true;
                if (platformType)
                {
                    targetY = 5f;
                }
            }
        }
    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (platformType)
            {
                if (!movePosition)
                {
                    rb.velocity = new Vector2(0f, -0.1f);
                }
                else
                {
                    rb.velocity = new Vector2(0f, -0.1f);
                }
                moveProgress += 0.1f;
            }
            if (moveProgress >= 5f)
            {
                isMoving = false;
                moveProgress = 0;
                movePosition = !movePosition;
            }
        }
        
    }
}
