using UnityEngine;

public class BossArenaManager : MonoBehaviour
{
    public BossController boss;

    private bool fightStarted = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (fightStarted == false && other.CompareTag("Player"))
        {
            fightStarted = true;

            //start fight
            if (boss != null)
            {
                boss.StartBossBattle();
            }
        }
    }

    public void OnBossDefeated()
    {
        return;
    }
}