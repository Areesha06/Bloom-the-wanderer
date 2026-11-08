using UnityEngine;
using UnityEngine.InputSystem;

public class PotionDrinker : MonoBehaviour
{
    public int healAmount = 3;
    private PlayerHealth health;

    void Awake() { health = GetComponent<PlayerHealth>(); }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null || !kb.hKey.wasPressedThisFrame) return;
        if (GameUI.Instance != null && GameUI.Instance.IsAnyPanelOpen) return;

        Inventory inv = Inventory.Instance;
        if (inv == null || inv.healthPotions <= 0) return;

        if (health.CurrentHealth >= health.maxHealth)
        {
            GameUI.Instance.ShowMessage("Your health is already full.");
            return;
        }

        inv.healthPotions--;
        health.Heal(healAmount);
        GameUI.Instance.ShowMessage("You drank the health potion!");
    }
}