using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Key : MonoBehaviour
{
    [SerializeField] private float preferredDistance = 0.5f;
    [SerializeField] private float attractionStrength = 12f;
    [SerializeField] private float repulsionStrength = 25f;
    [SerializeField] private float maxSpeed = 6f;

    public float sizeMultiplier;

    private Rigidbody2D rb;
    private Transform player;
    private bool isFollowing = false;

    public int index;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
        rb.linearDamping = 5f;
    }

    private void FixedUpdate()
    {
        if (!isFollowing || player == null) return;

        Vector2 pos = rb.position;
        Vector2 target = player.position;
        Vector2 toPlayer = target - pos;
        float dist = toPlayer.magnitude;

        if (dist > 0.001f)
        {
            Vector2 dir = toPlayer / dist;

            if (dist > preferredDistance)
            {
                float delta = dist - preferredDistance;
                rb.AddForce(dir * (delta * attractionStrength), ForceMode2D.Force);
            }
            
            float repelRadius = preferredDistance * 0.7f;
            if (dist < repelRadius)
            {
                float delta = repelRadius - dist;
                rb.AddForce(-dir * (delta * repulsionStrength), ForceMode2D.Force);
            }
        }

        if (rb.linearVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        player = other.transform;
        isFollowing = true;
        transform.localScale = new Vector3(sizeMultiplier,sizeMultiplier,sizeMultiplier);

        var col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled= false;
    }
}
