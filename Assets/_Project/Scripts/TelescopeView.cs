
using UnityEngine;
using UnityEngine.InputSystem;

public class TelescopeView : MonoBehaviour
{
    public StarRoomPuzzle puzzle;
    public Transform player;
    public PlayerController playerMovement;

    public GameObject telescopePanel;
    public RectTransform telescopeLens;
    public RectTransform targetStar;
    public GameObject roomStar;

    public float interactionDistance = 5f;
    public float moveSpeed = 250f;
    public float selectionDistance = 35f;

    private bool viewing = false;
    private bool found = false;

    void Start()
    {
        telescopePanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (!viewing)
        {
            // Open telescope using E after unlocking it.
            float distance = Vector2.Distance(
                player.position, transform.position
            );

            if (!found && puzzle.TelescopeUnlocked &&
                distance <= interactionDistance &&
                GameUI.Instance != null && !GameUI.Instance.IsAnyPanelOpen)
            {
                GameUI.Instance.SetHint("Press E to open the telescope");
            }

            if (!found && puzzle.TelescopeUnlocked &&
                distance <= interactionDistance &&
                Keyboard.current.eKey.wasPressedThisFrame)
            {
                OpenTelescope();
            }

            return;
        }

        // Move only the lens using WASD.
        Vector2 movement = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            movement.y += 1;

        if (Keyboard.current.sKey.isPressed)
            movement.y -= 1;

        if (Keyboard.current.aKey.isPressed)
            movement.x -= 1;

        if (Keyboard.current.dKey.isPressed)
            movement.x += 1;

        telescopeLens.anchoredPosition +=
            movement.normalized * moveSpeed * Time.deltaTime;

        // Keep the lens inside the telescope panel.
        RectTransform panelRect =
            telescopePanel.GetComponent<RectTransform>();

        Vector2 position = telescopeLens.anchoredPosition;

        float maxX = panelRect.rect.width / 2f;
        float maxY = panelRect.rect.height / 2f;

        position.x = Mathf.Clamp(position.x, -maxX, maxX);
        position.y = Mathf.Clamp(position.y, -maxY, maxY);

        telescopeLens.anchoredPosition = position;

        // Check if the lens is over the golden star.
        float starDistance = Vector2.Distance(
            telescopeLens.anchoredPosition,
            targetStar.anchoredPosition
        );

        if (Keyboard.current.spaceKey.wasPressedThisFrame &&
            starDistance <= selectionDistance)
        {
            found = true;
            roomStar.SetActive(true);

            Debug.Log("Star found!");
            CloseTelescope();
        }

        // Exit telescope with ESC.
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseTelescope();
        }
    }

    void OpenTelescope()
    {
        viewing = true;
        telescopePanel.SetActive(true);
        playerMovement.enabled = false;
    }

    void CloseTelescope()
    {
        viewing = false;
        telescopePanel.SetActive(false);
        playerMovement.enabled = true;
    }
}
