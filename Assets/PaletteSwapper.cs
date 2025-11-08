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
        display.CurrentPalette = 0;
    }
    public void SwapPaletteOne()
    {
        display.CurrentPalette = 1;
    }
    public void SwapPaletteTwo()
    {
        display.CurrentPalette = 2;
    }
    public void SwapPaletteThree()
    {
        display.CurrentPalette = 3;
    }
    public void SwapPaletteFor()
    {
        display.CurrentPalette = 4;
    }
    public void FinishSwapShader()
    {
        
    }
}
