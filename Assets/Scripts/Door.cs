using System;
using Unity.VisualScripting;
using UnityEngine;
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
    
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalPlayerInput = Input.GetAxisRaw("Vertical");
        if (verticalPlayerInput > 0 && hasDoorOpened)
        {
            if(!player.topDown)
                GoToRoomPosition();
            if(player.topDown)
                GoToHallwayPosition();
        }
    }

    private void FixedUpdate()
    {
        ChangingDoorSprites(isLocked, !hasDoorOpened, hasDoorOpened);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        hasPlayerEnteredDoor = true;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        hasPlayerEnteredDoor = false;
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

    public void GoToRoomPosition()
    {
        player.transform.position = roomTransform.position;
    }

    public void GoToHallwayPosition()
    {
        player.transform.position = hallwayPosition.position;
    }

    public void ChangingDoorSprites(bool lockedBool, bool closedBool, bool openBool)
    {
        lockedDoor.SetActive(lockedBool);
        closedDoor.SetActive(closedBool);
        openDoor.SetActive(openBool);
    }
}
