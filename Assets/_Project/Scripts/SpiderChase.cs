using UnityEngine;

public class SpiderChase : MonoBehaviour
{
    public PlayerHealth player;
    public LayerMask mazeWalls;
    public float speed = 1.5f;
    public float chaseRange = 4f;
    public float touchDistance = 0.6f;

    private float nextHitTime;

    void Update()
    {
        if (player == null)
            return;

        Vector2 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance > chaseRange ||
            Physics2D.Linecast(transform.position, player.transform.position, mazeWalls))
            return;

        if (distance <= touchDistance)
        {
            if (Time.time >= nextHitTime)
            {
                player.TakeDamage(1);
                nextHitTime = Time.time + 1f;
            }
            return;
        }

        Vector2 velocity = direction.normalized * speed;
        float step = Mathf.Min(speed * Time.deltaTime, distance - touchDistance);
        transform.position += (Vector3)(velocity.normalized * step);
    }
}