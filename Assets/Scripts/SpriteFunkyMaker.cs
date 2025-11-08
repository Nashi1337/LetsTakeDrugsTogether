using System;
using UnityEngine;

public class SpriteFunkyMaker : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float value = Mathf.PingPong(Time.time * 10f, 6f);
        spriteRenderer.material.color = Color.HSVToRGB(0, 0, value);
        Debug.Log(value);
    }
}
