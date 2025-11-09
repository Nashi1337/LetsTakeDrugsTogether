using System;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private float horizontalPlayerInput = 0;
    private float verticalPlayerInput = 0;
    public LayerMask rayLayer;
    public GameObject placeHolder;
    void Start()
    {
        
    }

    // Update is called once per frame
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Blocks"))
        {
            print(other.gameObject.name);
            Interactable interactable = other.gameObject.GetComponent<Interactable>();
            if (interactable != null && Input.GetKeyDown(KeyCode.E))
            {
                print("bluedabedidabedei");
                if (interactable != null)
                {
                    print(interactable.name);
                    interactable.InvokeMyEvent();
                }
            }
                
        }
    }

    void Update()
    {
        /**
        horizontalPlayerInput = Input.GetAxisRaw("Horizontal");
        verticalPlayerInput = Input.GetAxisRaw("Vertical");
        
        

        if (horizontalPlayerInput == verticalPlayerInput)
        return;
        
        Vector2 playerInput = new Vector2(horizontalPlayerInput, verticalPlayerInput);
        Vector3 playerPosition = new Vector3(transform.position.x + playerInput.x * 0.1f, transform.position.y + playerInput.y * 0.1f, -15f);
        placeHolder.transform.position = playerPosition;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(horizontalPlayerInput, verticalPlayerInput),1f, rayLayer );
        Debug.DrawRay(transform.position, new Vector2(horizontalPlayerInput, verticalPlayerInput), Color.red);
        if (hit)
        {
            print(hit.collider.name + " Hey im Here");
            if(hit.collider != null && Input.GetKeyDown(KeyCode.E))
            {
                print(hit.collider.name + "Big Mama");
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    print(interactable.name);
                    interactable.InvokeMyEvent();
                }
                   ;
            }
        }
        */
    }
}
