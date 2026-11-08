using UnityEngine;

public class FallZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph != null) ph.Respawn();
    }
}