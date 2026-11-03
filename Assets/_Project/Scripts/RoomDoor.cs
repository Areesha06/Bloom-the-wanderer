
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomDoor : MonoBehaviour
{
    public Transform destination;
    private Transform player;

    void Update()
    {
        if (player != null && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            player.position = destination.position;
            player = null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }
}
