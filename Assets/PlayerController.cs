using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D : MonoBehaviour
{
    private Rigidbody2D rb;
    private float inputX;
    private int facing = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(new Vector2(-0.01f, 0f), ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(new Vector2(0.01f, 0f), ForceMode2D.Impulse);
        }
    }
    
    private void OnMove(InputValue value)
    {
        Debug.Log("Hi");
    }
}
