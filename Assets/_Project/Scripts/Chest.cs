using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Chest : Interactable
{
    public string correctCode = "463";
    public GameObject codePanel;
    public TMP_Text codeText;
    public Color openedColor = new Color(1f, 0.85f, 0.2f);

    private string entered = "";
    private string feedback = "";
    private bool isOpen;
    private bool unlocked;

    static readonly Key[] digitKeys = {
        Key.Digit0, Key.Digit1, Key.Digit2, Key.Digit3, Key.Digit4,
        Key.Digit5, Key.Digit6, Key.Digit7, Key.Digit8, Key.Digit9 };
    static readonly Key[] numpadKeys = {
        Key.Numpad0, Key.Numpad1, Key.Numpad2, Key.Numpad3, Key.Numpad4,
        Key.Numpad5, Key.Numpad6, Key.Numpad7, Key.Numpad8, Key.Numpad9 };

    public override bool CanInteract => !unlocked;

    void Reset() { prompt = "Press E to enter code"; }

    void Start() { codePanel.SetActive(false); }

    public override void Interact()
    {
        isOpen = true;
        entered = "";
        feedback = "";
        codePanel.SetActive(true);
        GameUI.Instance.codePanelOpen = true;
        GameUI.Instance.MarkChange();
        Refresh();
    }

    void Update()
    {
        if (!isOpen) return;
        Keyboard kb = Keyboard.current;
        if (kb == null) return;
        if (Time.frameCount == GameUI.Instance.LastChangeFrame) return;

        if (kb.escapeKey.wasPressedThisFrame) { Close(); return; }

        if (kb.backspaceKey.wasPressedThisFrame && entered.Length > 0)
        {
            entered = entered.Substring(0, entered.Length - 1);
            feedback = "";
            Refresh();
        }

        for (int d = 0; d < 10; d++)
        {
            if (entered.Length < 3 &&
                (kb[digitKeys[d]].wasPressedThisFrame || kb[numpadKeys[d]].wasPressedThisFrame))
            {
                entered += d.ToString();
                feedback = "";
                Refresh();
            }
        }

        if ((kb.enterKey.wasPressedThisFrame || kb.numpadEnterKey.wasPressedThisFrame)
            && entered.Length == 3)
            Submit();
    }

    void Submit()
    {
        if (entered == correctCode)
        {
            unlocked = true;
            GetComponent<SpriteRenderer>().color = openedColor;
            Inventory.Instance.redShards++;
            GameUI.Instance.ShowMessage("The chest opened! Red shard collected!");
            Close();
        }
        else
        {
            feedback = "<color=#ff5555>Wrong code. Try again.</color>";
            entered = "";
            Refresh();
        }
    }

    void Close()
    {
        isOpen = false;
        codePanel.SetActive(false);
        GameUI.Instance.codePanelOpen = false;
        GameUI.Instance.MarkChange();
    }

    void Refresh()
    {
        string slots = "";
        for (int i = 0; i < 3; i++)
            slots += (i < entered.Length ? entered[i].ToString() : "_") + "  ";

        codeText.text = "ENTER THE 3-DIGIT CODE\n\n<size=150%>" + slots + "</size>\n\n" +
                        feedback + "\n\n<size=60%>Enter = confirm   Backspace = delete   Esc = close</size>";
    }
}