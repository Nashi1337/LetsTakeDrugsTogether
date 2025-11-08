using System;
using Unity.VisualScripting;
using UnityEngine;

public class anomalyFollower : MonoBehaviour
{
    public float preferredDistance = 0.2f;
    public float attractionStrength = 0.1f;
    public float repulsionStrength = 0.1f;
    public float speed = 1f;
    private Rigidbody2D rb;
    private AudioSource schmaudio;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if(rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();
        schmaudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            schmaudio.enabled = false;
            schmaudio.enabled = true;
            if (schmaudio != null)
            {
                Debug.Log("trying to play audio lol");
                schmaudio.Play();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 pos = rb.position;
            Vector2 target = other.transform.position;
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

            if (rb.linearVelocity.sqrMagnitude > 1f * 1f)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * speed;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        rb.linearVelocity = Vector2.zero;
        if(schmaudio!=null)
            schmaudio.Stop();
    }
}
