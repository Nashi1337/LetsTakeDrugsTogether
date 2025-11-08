using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Horizontal player keyboard input
    //  -1 = Left
    //   0 = No input
    //   1 = Right
    private float horizontalPlayerInput = 0;
    private float verticalPlayerInput = 0;

    [SerializeField] private float speed = 250;
    public bool topDown = false;
    
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ToggleTopDown()
    {
        topDown = !topDown;
        rb.gravityScale = topDown ? 0 : 1;
    }

    void Update()
    {
        horizontalPlayerInput = Input.GetAxisRaw("Horizontal");
        verticalPlayerInput = Input.GetAxisRaw("Vertical");

        SwapSprite();
    }

    void SwapSprite()
    {
        // Right
        if (horizontalPlayerInput > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        // Left
        else if (horizontalPlayerInput < 0)
        {
            transform.localScale = new Vector3(
                -1 * Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    void FixedUpdate()
    {
        if (!topDown)
        {
            float vx = horizontalPlayerInput * speed;
            rb.linearVelocity = new Vector2(vx, rb.linearVelocityY);
        }
        else
        {
            float h = horizontalPlayerInput;
            float v = verticalPlayerInput;

            if (Mathf.Abs(h) > Mathf.Abs(v))
            {
                rb.linearVelocity = new Vector2(h * speed, 0f);
            }
            else if (Mathf.Abs(v) > 0f)
            {
                rb.linearVelocity = new Vector2(0f, v * speed);
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TopDownTriggerZone"))
        {
            ToggleTopDown();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TopDownTriggerZone"))
        {
            ToggleTopDown();
        }
    }
}