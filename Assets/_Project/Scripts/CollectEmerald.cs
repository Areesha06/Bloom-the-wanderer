using UnityEngine;

public class CollectEmerald : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory.Instance.hasEmerald = true;
            Destroy(gameObject);
        }
    }
}