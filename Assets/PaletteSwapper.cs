using System;
using System.Collections;
using System.Collections.Generic;
using GBTemplate;
using UnityEngine;

public class PaletteSwapper : MonoBehaviour
{
    [SerializeField] private GBDisplayController display;
    [SerializeField] private float swapInterval = 0.1f;

    private Coroutine paletteRoutine;

    private void Awake()
    {
        if (display == null)
            display = GetComponent<GBDisplayController>();
    }
    
    private void OnDisable()
    {
        if (paletteRoutine != null)
            StopCoroutine(paletteRoutine);
    }



    public void SwapPaletteStandard()
    {
        display.UpdateColorPalette(0);
    }
    public void SwapPaletteOne()
    {
        display.UpdateColorPalette(1);
    }
    public void SwapPaletteTwo()
    {
        display.UpdateColorPalette(37);
    }
    public void SwapPaletteThree()
    {
        display.UpdateColorPalette(3);
    }
    public void SwapPaletteFor()
    {
        display.UpdateColorPalette(4);
    }
    public void FinishSwapShader()
    {
        
    }

    public void SwapIntoRandomPalette()
    {
        int myPalette = UnityEngine.Random.Range(1,37);
        display.UpdateColorPalette(myPalette);
    }
}
