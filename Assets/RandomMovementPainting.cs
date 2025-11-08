using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class RandomMovementPainting : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float wanderDirectionChangeInterval = 1.5f;

    [Header("Flee Behaviour")]
    [SerializeField] private float fleeRadius = 4f;           // distance at which it starts fleeing
    [SerializeField] private string playerTag = "Player";

    [Header("Obstacle Avoidance")]
    [SerializeField] private LayerMask obstacleMask;          // walls/obstacles
    [SerializeField] private float obstacleCheckDistance = 0.8f;

    private Rigidbody2D rb;
    private Transform player;
    private Vector2 wanderDirection = Vector2.right;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
        rb.linearDamping = 3f;  // smooth floaty movement
    }

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning($"FleeingWanderer2D on {name}: no object with tag '{playerTag}' found.");

        StartCoroutine(ChangeWanderDirectionRoutine());
    }

    private IEnumerator ChangeWanderDirectionRoutine()
    {
        while (true)
        {
            wanderDirection = Random.insideUnitCircle.normalized;
            yield return new WaitForSeconds(wanderDirectionChangeInterval);
        }
    }

    private void FixedUpdate()
    {
        Vector2 desiredDir = wanderDirection;

        if (player != null)
        {
            Vector2 toPlayer = (Vector2)player.position - rb.position;
            float dist = toPlayer.magnitude;

            if (dist < fleeRadius && dist > 0.01f)
            {
                // Base flee direction: directly away from player
                Vector2 fleeDir = (-toPlayer).normalized;

                // Try to move in that direction; if blocked, slide along wall
                desiredDir = GetAvoidanceDirection(fleeDir);
            }
        }

        rb.linearVelocity = desiredDir * moveSpeed;
    }

    private Vector2 GetAvoidanceDirection(Vector2 desiredDir)
    {
        // Check for obstacle straight ahead
        RaycastHit2D hit = Physics2D.Raycast(rb.position, desiredDir, obstacleCheckDistance, obstacleMask);

        if (!hit)
        {
            // Nothing in the way → go as planned
            return desiredDir;
        }

        // There is a wall. Compute a tangent direction along the wall surface.
        // Wall normal:
        Vector2 normal = hit.normal;

        // Two possible tangents: perpendicular in each direction
        Vector2 tangent1 = new Vector2(-normal.y, normal.x);   // Vector2.Perpendicular(normal)
        Vector2 tangent2 = -tangent1;

        // Choose the tangent that also moves us more away from the player
        Vector2 bestTangent = tangent1;

        if (player != null)
        {
            Vector2 fromPlayer = rb.position - (Vector2)player.position;
            // Dot product: higher = more aligned with fromPlayer (away from player)
            float d1 = Vector2.Dot(tangent1, fromPlayer);
            float d2 = Vector2.Dot(tangent2, fromPlayer);

            bestTangent = d1 >= d2 ? tangent1 : tangent2;
        }

        return bestTangent.normalized;
    }

    private void OnDrawGizmosSelected()
    {
        if (rb == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rb.position, fleeRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            Destroy(gameObject);
    }
}
