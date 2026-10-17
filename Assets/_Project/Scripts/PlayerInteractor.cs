using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerInteractor : MonoBehaviour
{
    private List<Interactable> nearby = new List<Interactable>();

    void OnTriggerEnter2D(Collider2D other)
    {
        Interactable i = other.GetComponent<Interactable>();
        if (i != null && !nearby.Contains(i)) nearby.Add(i);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Interactable i = other.GetComponent<Interactable>();
        if (i != null) nearby.Remove(i);
    }

    void Update()
    {
        GameUI ui = GameUI.Instance;
        if (ui == null) return;

        nearby.RemoveAll(x => x == null);

        Interactable target = null;
        foreach (Interactable i in nearby)
        {
            if (i.CanInteract) { target = i; break; }
        }

        if (target == null || ui.IsAnyPanelOpen)
        {
            ui.SetPrompt("");
            return;
        }

        ui.SetPrompt(target.prompt);

        Keyboard kb = Keyboard.current;
        if (kb != null && kb.eKey.wasPressedThisFrame && Time.frameCount != ui.LastChangeFrame)
            target.Interact();
    }
}