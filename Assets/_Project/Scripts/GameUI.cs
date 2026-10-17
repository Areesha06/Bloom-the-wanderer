using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    [Header("Panels")]
    public GameObject cluePanel;
    public TMP_Text clueText;
    public GameObject inventoryPanel;
    public TMP_Text inventoryText;
    public ScrollRect inventoryScroll;

    [Header("HUD text")]
    public TMP_Text messageText;
    public TMP_Text promptText;

    [Header("Player")]
    public PlayerController player;

    [HideInInspector] public bool codePanelOpen;
    public int LastChangeFrame { get; private set; } = -1;

    public bool IsAnyPanelOpen =>
        cluePanel.activeSelf || inventoryPanel.activeSelf || codePanelOpen;

    void Awake() { Instance = this; }

    void Start()
    {
        cluePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        messageText.text = "";
        promptText.text = "";
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb != null && Time.frameCount != LastChangeFrame)
        {
            if (cluePanel.activeSelf &&
                (kb.eKey.wasPressedThisFrame || kb.escapeKey.wasPressedThisFrame))
                CloseClue();
            else if (inventoryPanel.activeSelf &&
                (kb.iKey.wasPressedThisFrame || kb.escapeKey.wasPressedThisFrame))
                CloseInventory();
            else if (!IsAnyPanelOpen && kb.iKey.wasPressedThisFrame)
                OpenInventory();
        }
        if (inventoryPanel.activeSelf && inventoryScroll != null && kb != null)
        {
            float dir = 0f;
            if (kb.wKey.isPressed || kb.upArrowKey.isPressed) dir += 1f;
            if (kb.sKey.isPressed || kb.downArrowKey.isPressed) dir -= 1f;
            if (dir != 0f)
                inventoryScroll.verticalNormalizedPosition =
                    Mathf.Clamp01(inventoryScroll.verticalNormalizedPosition + dir * 0.8f * Time.unscaledDeltaTime);
        }
        
        if (player != null) player.inputLocked = IsAnyPanelOpen;
    }

    public void MarkChange() { LastChangeFrame = Time.frameCount; }

    public void ShowClue(string title, string body)
    {
        clueText.text = "<b>" + title + "</b>\n\n" + body +
                        "\n\n<size=70%><i>Press E to close</i></size>";
        cluePanel.SetActive(true);
        MarkChange();
    }

    void CloseClue() { cluePanel.SetActive(false); MarkChange(); }

    void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        inventoryText.text = Inventory.Instance.BuildSummary();
        LayoutRebuilder.ForceRebuildLayoutImmediate(inventoryText.rectTransform);
        if (inventoryScroll != null) inventoryScroll.verticalNormalizedPosition = 1f;
        MarkChange();
    }

    void CloseInventory() { inventoryPanel.SetActive(false); MarkChange(); }

    public void SetPrompt(string text) { promptText.text = text; }

    public void ShowMessage(string msg)
    {
        StopAllCoroutines();
        StartCoroutine(MessageRoutine(msg));
    }

    IEnumerator MessageRoutine(string msg)
    {
        messageText.text = msg;
        yield return new WaitForSeconds(2.5f);
        messageText.text = "";
    }
}