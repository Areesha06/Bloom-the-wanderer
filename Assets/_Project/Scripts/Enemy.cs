using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public Projectile projectilePrefab;
    public float shootInterval = 2f;
    public float detectRange = 10f;

    private Transform player;
    private SpriteRenderer sr;
    private Color originalColor;
    private float nextShot;

    void Start()
    {
        PlayerController pc = FindFirstObjectByType<PlayerController>();
        if (pc != null) player = pc.transform;

        sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null) originalColor = sr.color;

        nextShot = Time.time + 1f;
    }

    void Update()
    {
        if (player == null || projectilePrefab == null) return;

        float dx = player.position.x - transform.position.x;
        float dy = Mathf.Abs(player.position.y - transform.position.y);

        if (Mathf.Abs(dx) <= detectRange && dy < 3f && Time.time >= nextShot)
        {
            nextShot = Time.time + shootInterval;
            Shoot(Mathf.Sign(dx));
        }
    }

    void Shoot(float dir)
    {
        Vector3 spawnPos = transform.position + new Vector3(0.8f * dir, 0f, 0f);
        Projectile p = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        p.Launch(new Vector2(dir, 0f));
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        StopAllCoroutines();
        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }
        StartCoroutine(HitFlash());
    }

    IEnumerator HitFlash()
    {
        if (sr != null) sr.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        if (sr != null) sr.color = originalColor;
    }
}