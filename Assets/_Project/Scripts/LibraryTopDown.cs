using UnityEngine;
using UnityEngine.InputSystem;

public class LibraryTopDownMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool libraryMode = false;

    private float normalGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalGravity = rb.gravityScale;
    }

    void Update()
    {
        if (!libraryMode)
            return;

        float x = 0f;
        float y = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            x = -1f;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            x = 1f;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            y = 1f;

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            y = -1f;

        moveInput = new Vector2(x, y).normalized;
    }

    void FixedUpdate()
    {
        if (!libraryMode)
            return;

        rb.linearVelocity = moveInput * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "LibraryRoomTrigger")
        {
            libraryMode = true;
            rb.gravityScale = 0f;
            rb.linearVelocity = Vector2.zero;

            Debug.Log("Library top-down movement ON");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "LibraryRoomTrigger")
        {
            libraryMode = false;
            rb.gravityScale = normalGravity;
            rb.linearVelocity = Vector2.zero;
            moveInput = Vector2.zero;

            Debug.Log("Library top-down movement OFF");
        }
    }
}