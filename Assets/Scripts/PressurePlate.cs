using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
     public UnityEvent myEvent = new UnityEvent();
     
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
                DoMyEvent();
            }
        }
    }
    
    
    
}
