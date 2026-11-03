
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject requiredObject;
    public bool isPressed = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == requiredObject)
        {
            isPressed = true;
            spriteRenderer.color = Color.green;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == requiredObject)
        {
            isPressed = false;
            spriteRenderer.color = originalColor;
        }
    }
}
