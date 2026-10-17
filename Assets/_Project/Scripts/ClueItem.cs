using UnityEngine;

public class ClueItem : Interactable
{
    public string clueTitle = "Clue";
    [TextArea(3, 8)] public string clueBody;

    void Reset() { prompt = "Press E to read"; }

    public override void Interact()
    {
        bool isNew = Inventory.Instance.AddClue(clueTitle, clueBody);
        GameUI.Instance.ShowClue(clueTitle, clueBody);
        if (isNew) GameUI.Instance.ShowMessage("Clue added to inventory");
    }
}