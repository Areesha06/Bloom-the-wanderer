using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerController))]
public class LibraryTopDownMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool IsInLibrary { get; private set; }

    private Rigidbody2D rb;
    private PlayerController playerController;
    private float normalGravity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (!IsInLibrary)
            return;

        var keyboard = Keyboard.current;
        if (keyboard == null || playerController.inputLocked)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = Vector2.zero;

        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            direction.x -= 1;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            direction.x += 1;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
            direction.y -= 1;
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
            direction.y += 1;

        rb.linearVelocity = direction.normalized * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsInLibrary || other.gameObject.name != "LibraryRoomTrigger")
            return;

        normalGravity = rb.gravityScale;
        IsInLibrary = true;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IsInLibrary || other.gameObject.name != "LibraryRoomTrigger")
            return;

        IsInLibrary = false;
        rb.gravityScale = normalGravity;
        rb.linearVelocity = Vector2.zero;
    }
}