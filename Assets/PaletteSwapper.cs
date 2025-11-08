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

    private void OnEnable()
    {
        paletteRoutine = StartCoroutine(PaletteLoop());
    }

    private void OnDisable()
    {
        if (paletteRoutine != null)
            StopCoroutine(paletteRoutine);
    }

    private IEnumerator PaletteLoop()
    {
        while (true)
        {
            display.PaletteCycleNext();
            yield return new WaitForSeconds(swapInterval);
        }
    }
}
