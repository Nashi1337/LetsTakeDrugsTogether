using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent myEvent;

    public void InvokeMyEvent()
    {
        myEvent.Invoke();
    }
}
