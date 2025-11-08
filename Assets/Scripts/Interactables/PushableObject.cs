using System.IO.IsolatedStorage;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    
    private FixedJoint2D joint;
    public bool isAttached = false;

    private Rigidbody2D rb;
    private Collider2D col;
    private PlayerController player;

    public float maxTime = 1f;
    public float currentTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void Update()
    {
        currentTime -= Time.deltaTime;
        
        if(!isAttached)
            return;
        
        if (Input.GetKeyDown(KeyCode.E) && currentTime <= 0 )
        {
            DetachFromPlayer();
        }

        if (isAttached)
        {
            GameObject placeHolder = player.GetComponent<Interaction>().placeHolder;
            transform.position = placeHolder.transform.position;
        }
    }

    private void DetachFromPlayer()
    {
        gameObject.transform.parent = null;
        isAttached = false;
    }

    public void PushMyThing()
    {
        if (currentTime <= 0)
        {
            isAttached = true;
            print("uwu");
            currentTime = maxTime;
        }
    }
}
