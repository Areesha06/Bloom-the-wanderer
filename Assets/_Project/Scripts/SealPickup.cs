using UnityEngine;

public class SealPickup : Interactable
{
    public GameObject[] mustBeCleared;

    void Reset() { prompt = "Press E to take the seal"; }

    bool AllCleared()
    {
        foreach (GameObject g in mustBeCleared)
            if (g != null) return false;
        return true;
    }

    public override void Interact()
    {
        if (!AllCleared())
        {
            GameUI.Instance.ShowMessage("The seal is buried under webs and spiders!");
            return;
        }
        Inventory.Instance.hasSeal = true;
        GameUI.Instance.ShowMessage("You found the royal seal!");
        Destroy(gameObject);
    }
}