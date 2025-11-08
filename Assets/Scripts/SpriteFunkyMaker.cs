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
        SIZE
    }
    
    public funkyModes funkyMode;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        funkyMode = (funkyModes)Random.Range(0, 4);
    }

    void Update()
    {
        switch (funkyMode)
        {
            case funkyModes.NONE: break;
            case funkyModes.HUE:
                float value = Mathf.PingPong(Time.time * 10f, 6f);
                spriteRenderer.material.color = Color.HSVToRGB(0, 0, value);
                break;
            case funkyModes.ROTATION:
                transform.rotation = Quaternion.Euler(0f, 0f, Mathf.PingPong(Time.time * 100f, 359f));
                break;
            case funkyModes.SIZE:
                float size = Mathf.PingPong(Time.time * 1f, 1.1f);
                transform.localScale = new Vector3(size, size, size);
                break;
            default:
                break;
        }

    }
}
