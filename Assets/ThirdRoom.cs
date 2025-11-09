using UnityEngine;
using UnityEngine.Events;

public class ThirdRoom : MonoBehaviour
{
    public bool shrek;
    public bool kermit;

    public UnityEvent SexRoomSequence;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shrek && kermit)
        {
            SexRoomSequence.Invoke();
        }
    }
    
    
    
}
