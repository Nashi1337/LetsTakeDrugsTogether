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

    private bool justTeleported = false;
    private float lastTeleportTime = 999f;
    private float teleportCooldown = 0.1f;
    
    public CameraController cameraController;
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

        if(!topDown)
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
            CameraPosition targetTransform = other.gameObject.GetComponentInChildren<CameraPosition>();
            cameraController.currentPosition = targetTransform.gameObject.transform;
        }

        if (other.CompareTag("Key"))
        {
            KeyEvent keyEvent = other.GetComponent<KeyEvent>();
            if (other.GetComponent<KeyEvent>() != null)
            {
                keyEvent.DoEvent();
            }
        }

        if (other.CompareTag("Teleporter"))
        {
            if (justTeleported && Time.time - lastTeleportTime < teleportCooldown)
                return;
            var tp = other.GetComponent<teleporter>();
            if (tp == null || tp.whereTo == null)
                return;

            bool teleportToLeft = tp.whereTo.position.x < other.GetComponentInParent<Transform>().position.x;
            if (teleportToLeft && horizontalPlayerInput < 0)
                return;
            if (!teleportToLeft && horizontalPlayerInput > 0)
                return;
            float offsetX = transform.position.x - other.transform.position.x;
            transform.position = new Vector3(
                tp.whereTo.position.x + offsetX,
                transform.position.y,
                transform.position.z
            );
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TopDownTriggerZone"))
        {
            ToggleTopDown();
            cameraController.currentPosition = cameraController.playerPosition;
        }
    }
}