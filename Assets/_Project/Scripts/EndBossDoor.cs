using UnityEngine;

public class EndBossDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public float openHeight = 3.5f; 
    public float openSpeed = 2f;    

    private Vector3 closedPosition;
    private Vector3 targetPosition;
    private bool opening = false;
    private bool kingWasFound = false;

    void Start()
    {
        closedPosition = transform.position;
        targetPosition = closedPosition + new Vector3(0f, openHeight, 0f);

        if (GameObject.Find("BossKing") != null)
        {
            kingWasFound = true;
        }
    }

    void Update()
    {
        if (!opening && kingWasFound)
        {
            if (GameObject.Find("BossKing") == null)
            {
                opening = true;
            }
        }
        if (opening)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, openSpeed * Time.deltaTime);
        }
    }
}