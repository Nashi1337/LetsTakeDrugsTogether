using UnityEngine;
using UnityEngine.Events;

public class KeyEvent : MonoBehaviour
{
    public UnityEvent myEvent = new UnityEvent();
    public AudioClip SFXcollected;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoEvent()
    {
        myEvent.Invoke();
        this.gameObject.SetActive(true);
        AudioManager.Instance.PlaySFX(SFXcollected);
    }
}
