using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform currentPosition;
    public Transform playerPosition;

    public PlayerController player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerPosition = player.GetComponentInChildren<CameraPosition>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = currentPosition.position;
    }
}
