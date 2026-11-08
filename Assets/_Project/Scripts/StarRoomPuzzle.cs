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
    public float hintDistance = 3f;

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

        ShowHint();

        // Both pressure plates must be activated.
        if (!plate1.isPressed || !plate2.isPressed)
            return;

        // Player must be close to the mirror.
        float distance = Vector2.Distance(player.position, mirror.position);
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

            if (GameUI.Instance != null)
                GameUI.Instance.ShowMessage("The telescope is powered on!");

            Debug.Log("Star Room puzzle solved!");
        }
    }

    void ShowHint()
    {
        GameUI ui = GameUI.Instance;
        if (ui == null || ui.IsAnyPanelOpen) return;

        // Only show hints near the second pressure plate
        if (Vector2.Distance(player.position, plate2.transform.position) > hintDistance)
            return;

        if (!plate1.isPressed)
            ui.SetHint("Push the heavy block onto the first plate");
        else if (!plate2.isPressed)
            ui.SetHint("Stand on the second pressure plate");
        else if (Inventory.Instance == null || Inventory.Instance.redShards <= 0)
            ui.SetHint("You need the red shard to power the mirror");
        else if (Vector2.Distance(player.position, mirror.position) > interactionDistance)
            ui.SetHint("Move closer to the mirror");
        else
            ui.SetHint("Press F to shine light and activate the telescope");
    }
}