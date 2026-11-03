
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class DarkRoomLighting : MonoBehaviour
{
    public Light2D globalLight;
    public Light2D shardLight;

    private bool playerInside = false;

    void Start()
    {
        shardLight.enabled = false;
    }

    
    void Update()
    {
        if (playerInside && Keyboard.current != null &&
            Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (Inventory.Instance.redShards > 0)
            {
                shardLight.enabled = !shardLight.enabled;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            globalLight.intensity = 0.02f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            globalLight.intensity = 1f;
            shardLight.enabled = false;
        }
    }
}
