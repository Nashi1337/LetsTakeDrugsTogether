using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PushPullBlock : MonoBehaviour
{
    private FixedJoint2D joint;
    private bool isAttached = false;

    private Rigidbody2D rb;
    private Collider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
            return;

        if (!isAttached)
        {
            AttachToPlayer(collision.collider);
        }
        else
        {
            DetachFromPlayer();
        }
    }

    private void AttachToPlayer(Collider2D playerCollider)
    {
        Rigidbody2D playerRb = playerCollider.attachedRigidbody;
        if (playerRb == null) return;

        joint = gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = playerRb;

        joint.autoConfigureConnectedAnchor = true;

        isAttached = true;
    }

    private void DetachFromPlayer()
    {
        if (joint != null)
        {
            Destroy(joint);
            joint = null;
        }

        isAttached = false;
    }
}