using UnityEngine;

public class SexPuzzlePiece : MonoBehaviour
{
    public Transform targetSlot;
    public float snapDistance = 0.1f;

    public bool IsInCorrectPosition { get; private set; }
    private Rigidbody2D rb;

    public AudioClip SEX_SFX;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (targetSlot == null)
            return;

        float dist = Vector2.Distance(transform.position, targetSlot.position);

        if (dist <= snapDistance)
        {
            IsInCorrectPosition = true;
            transform.position = targetSlot.position;

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
        else
        {
            IsInCorrectPosition = false;
        }
    }
}