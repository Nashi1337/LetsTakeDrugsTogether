using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Door : MonoBehaviour
{
    public Transform hallwayPosition;
    [FormerlySerializedAs("roomPosition")] public Transform roomTransform;

    public bool isLocked;
    public bool hasPlayerEnteredDoor;
    public bool hasDoorOpened;
    public Animator doorAnimator;

    public float verticalPlayerInput;
    private PlayerController player;
    
    [SerializeField] private GameObject lockedDoor;
    [SerializeField] private GameObject closedDoor;
    [SerializeField] private GameObject openDoor;

    public enum DoorState { NotEntered, Entered, Finished};
    public DoorState currentDoorState = DoorState.NotEntered;
    public UnityEvent myEvent;
    public UnityEvent EnteredRoomEvent;
    
    void Start()
    {
        if(GameObject.Find("Player"))
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked == false)
        {
            verticalPlayerInput = Input.GetAxisRaw("Vertical");
            if (verticalPlayerInput > 0 && hasDoorOpened)
            {
                if (!player.topDown)
                {
                    GoToRoomPosition();
                    if(currentDoorState != DoorState.Finished)
                    currentDoorState = DoorState.Entered;
                    EnteredRoomEvent.Invoke();
                }
            }

            if (verticalPlayerInput < 0 && hasPlayerEnteredDoor && currentDoorState == DoorState.Finished)
            {
                if(player.topDown)
                    GoToHallwayPosition();
            }
        }
       
    }

    private void FixedUpdate()
    {
        ChangingDoorSprites(isLocked, !hasDoorOpened, hasDoorOpened);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayerEnteredDoor = true;
            hasDoorOpened = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayerEnteredDoor = false;
            hasDoorOpened = false;
        }
    }

    public void TryDoorAnimation()
    {
        if (hasPlayerEnteredDoor && !isLocked)
        {
            DoDoorAnimation();
        }
    }

    public void DoDoorAnimation()
    {
        
    }

    public void OpenDoor()
    {
        hasDoorOpened = true;
    }

    public void CloseDoor()
    {
        hasDoorOpened = false;
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }

    public void GoToRoomPosition()
    {
        player.transform.position = roomTransform.position;
        hasDoorOpened = false;
    }

    public void GoToHallwayPosition()
    {
        player.transform.position = hallwayPosition.position;
    }

    public void ChangingDoorSprites(bool lockedBool, bool closedBool, bool openBool)
    {
        lockedDoor.SetActive(lockedBool);
        if (lockedBool == false)
        {
            closedDoor.SetActive(closedBool);
            openDoor.SetActive(openBool);
        }
    }

    public void RoomFinished()
    {
        if(currentDoorState != DoorState.Finished)
        myEvent.Invoke();
       currentDoorState = DoorState.Finished;
        
    }
}
