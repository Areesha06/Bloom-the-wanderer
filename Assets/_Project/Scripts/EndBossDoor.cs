using UnityEngine;

public class EndBossDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public float openHeight = 3.5f; // How far up it slides
    public float openSpeed = 2f;    // Sliding speed

    private Vector3 closedPosition;
    private Vector3 targetPosition;
    private bool opening = false;
    private bool kingWasFound = false;

    void Start()
    {
        closedPosition = transform.position;
        targetPosition = closedPosition + new Vector3(0f, openHeight, 0f);

        // Check if King is present at the start of the scene
        if (GameObject.Find("BossKing") != null)
        {
            kingWasFound = true;
        }
    }

    void Update()
    {
        // 1. Detect when the king dies
        if (!opening && kingWasFound)
        {
            if (GameObject.Find("BossKing") == null)
            {
                opening = true;
            }
        }

        // 2. Slide smoothly upwards
        if (opening)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, openSpeed * Time.deltaTime);
        }
    }
}