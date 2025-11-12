using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
     public UnityEvent myEvent = new UnityEvent();
    public AudioClip SFX;
    void Update()
    {
        
    }

    public void DoMyEvent()
    {
        myEvent.Invoke();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Blocks"))
        {
            bool isAttached = other.GetComponent<PushableObject>().isAttached;
            if (!isAttached)
            {
                AudioManager.Instance.PlaySFX(SFX);
                DoMyEvent();
            }
        }
    }
    
    
    
}
