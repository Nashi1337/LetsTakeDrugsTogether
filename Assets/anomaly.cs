using System;
using UnityEngine;

public class anomaly : MonoBehaviour
{
    public bool isAnomaly;

    private void Awake()
    {
        var spriteFunkyMaker = GetComponent<SpriteFunkyMaker>();
        if(spriteFunkyMaker!=null)
            isAnomaly = spriteFunkyMaker.funkyMode != SpriteFunkyMaker.funkyModes.NONE;
    }
}
