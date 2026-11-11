using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public BossController boss;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player stepped into the room
        if (other.CompareTag("Player") && boss != null)
        {
            boss.StartBossBattle();
            gameObject.SetActive(false); // Disable trigger so it only fires once
        }
    }
}