using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpriteFunkyMaker : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public enum funkyModes
    {
        NONE,
        HUE,
        ROTATION,
        SIZE,
        RANDOM
    }

    public float magnitude = 1f;
    public float speed = 1f;
    
    public funkyModes funkyMode;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(funkyMode==funkyModes.RANDOM) funkyMode = (funkyModes)Random.Range(0, 4);
    }

    void Update()
    {
        switch (funkyMode)
        {
            case funkyModes.NONE: break;
            case funkyModes.HUE:
                float value = Mathf.PingPong(Time.time * speed, 6f);
                spriteRenderer.material.color = Color.HSVToRGB(0, 0, value);
                break;
            case funkyModes.ROTATION:
                transform.rotation = Quaternion.Euler(0f, 0f, Mathf.PingPong(Time.time * speed, 2f*magnitude)-magnitude);
                break;
            case funkyModes.SIZE:
                float size = Mathf.PingPong(Time.time * speed, 1.1f);
                transform.localScale = new Vector3(size, size, size);
                break;
            case funkyModes.RANDOM:
                funkyMode = funkyMode = (funkyModes)Random.Range(0, 4);
                break;
            default:
                break;
        }

    }
}
