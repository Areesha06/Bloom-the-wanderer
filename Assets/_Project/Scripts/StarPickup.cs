using UnityEngine;

public class StarPickup : Interactable
{
    void Reset() { prompt = "Press E to take the star"; }

    public override void Interact()
    {
        Inventory.Instance.hasStar = true;
        GameUI.Instance.ShowMessage("You captured a star!");
        Destroy(gameObject);
    }
}