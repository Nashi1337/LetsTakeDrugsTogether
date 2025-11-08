using System;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    
    public Animator anim;

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
        
        anim.SetFloat("Horizontal", horizontalPlayerInput);
        anim.SetFloat("Vertical", verticalPlayerInput);
        anim.SetFloat("Speed", Mathf.Abs((verticalPlayerInput + horizontalPlayerInput) * speed));

        if(!topDown)
            SwapSprite();
    }

    void SwapSprite()
    {
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
            if(cameraController != null)
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Anomaly"))
        {
            if(Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TopDownTriggerZone"))
        {
            ToggleTopDown();
            if(cameraController != null)
                cameraController.currentPosition = cameraController.playerPosition;
        }
    }
}