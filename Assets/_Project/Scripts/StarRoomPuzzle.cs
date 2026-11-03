
using UnityEngine;
using UnityEngine.InputSystem;

public class StarRoomPuzzle : MonoBehaviour
{
    public PressurePlate plate1;
    public PressurePlate plate2;

    public Transform player;
    public Transform mirror;

    public SpriteRenderer telescope;
    public GameObject star;

    public float interactionDistance = 3f;

    private bool solved = false;
    public bool TelescopeUnlocked => solved;

    void Start()
    {
        star.SetActive(false);
    }

    void Update()
    {
        if (solved)
            return;

        // Both pressure plates must be activated.
        if (!plate1.isPressed || !plate2.isPressed)
            return;

        // Player must be close to the mirror.
        float distance = Vector2.Distance(
            player.position, mirror.position
        );

        if (distance > interactionDistance)
            return;

        // Press F while carrying the red shard.
        if (Keyboard.current != null &&
            Keyboard.current.fKey.wasPressedThisFrame &&
            Inventory.Instance != null &&
            Inventory.Instance.redShards > 0)
        {
            solved = true;

            telescope.color = Color.green;

            Debug.Log("Star Room puzzle solved!");
        }
    }
}
