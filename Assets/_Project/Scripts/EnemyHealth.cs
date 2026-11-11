using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 20;

    void OnTriggerEnter2D(Collider2D other)
    {
        CheckHit(other.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CheckHit(collision.gameObject);
    }

    void CheckHit(GameObject thingThatHitMe)
    {
        string hitName = thingThatHitMe.name.ToLower();

        if (hitName.Contains("blast") || hitName.Contains("attack") || hitName.Contains("projectile"))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health = health - damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}