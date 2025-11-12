using UnityEngine;
using UnityEngine.Events;

public class SecondRoom : MonoBehaviour
{
    public int shrekCounter = 0;
    public UnityEvent myEvent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shrekCounter >= 2)
        {
            myEvent.Invoke();
        }
    }

    public void IncreaseShrekCounter()
    {
        shrekCounter = shrekCounter + 1;
    }
}
