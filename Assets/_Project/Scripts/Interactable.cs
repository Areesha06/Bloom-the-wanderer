using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string prompt = "Press E to interact";
    public virtual bool CanInteract => true;
    public abstract void Interact();
}