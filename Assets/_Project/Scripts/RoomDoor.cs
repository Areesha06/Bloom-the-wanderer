using UnityEngine;

public class RoomDoor : Interactable
{
    public Transform destination;
    public bool setAsRespawnPoint = true;
    public bool requiresRedShard = false;
    public string lockedMessage = "The door is locked.";
    public string doorPrompt = "Press E to enter";

    void Awake()
    {
        prompt = doorPrompt;
    }

    public override void Interact()
    {
        GameUI ui = GameUI.Instance;

        if (destination == null)
        {
            ui.ShowMessage("This door is locked for now.");
            return;
        }

        if (requiresRedShard && Inventory.Instance.redShards <= 0)
        {
            ui.ShowMessage(lockedMessage);
            return;
        }

        PlayerController pc = FindFirstObjectByType<PlayerController>();
        if (pc == null) return;

        pc.transform.position = destination.position;

        Rigidbody2D rb = pc.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        if (setAsRespawnPoint)
        {
            PlayerHealth ph = pc.GetComponent<PlayerHealth>();
            if (ph != null) ph.SetRespawnPoint(destination);
        }

        CameraFollow cam = FindFirstObjectByType<CameraFollow>();
        if (cam != null) cam.SnapToTarget();
    }
}