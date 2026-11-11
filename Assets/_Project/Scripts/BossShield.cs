using UnityEngine;

public class BossShield : MonoBehaviour
{
    void Update()
    {
        bool minionAlive = false;

        foreach (GameObject obj in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            if (obj.name.ToLower().Contains("skullgoon"))
            {
                minionAlive = true;
                break;
            }
        }

        //shield remove
        if (!minionAlive)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HandleBlock(other.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HandleBlock(collision.gameObject);
    }

    void HandleBlock(GameObject incomingObject)
    {
        string hitName = incomingObject.name.ToLower();

        if (hitName.Contains("blast") || hitName.Contains("projectile") || hitName.Contains("bullet"))
        {
            Destroy(incomingObject);
        }
    }
}