using UnityEngine;
using UnityEngine.InputSystem;

public class HeartShard : MonoBehaviour
{
    [Header("Idle Bobbing")]
    public float floatSpeed = 2.5f;
    public float floatHeight = 0.2f;

    private Vector3 startPosition;
    private bool playerInRange = false;
    private GameObject playerObj;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        //floating motion
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        Keyboard keyboard = Keyboard.current;
        if (playerInRange && keyboard != null && keyboard.eKey.wasPressedThisFrame)
        {
            Equip();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            playerObj = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            playerObj = null;
        }
    }

    void Equip()
    {
        if (playerObj != null)
        {

            playerObj.SendMessage("EquipHeartShard", SendMessageOptions.DontRequireReceiver);
        }
        Destroy(gameObject);
    }
}