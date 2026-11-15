using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public BossController boss;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && boss != null)
        {
            boss.StartBossBattle();
            gameObject.SetActive(false); 
        }
    }
}