using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public float invincibleTime = 1f;
    public Transform respawnPoint;
    public CameraFollow cam;
    public HealthBar healthBar;

    public int CurrentHealth { get; private set; }

    private bool invincible;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        CurrentHealth = maxHealth;
    }

    void Start()
    {
        UpdateBar();
    }

    public void TakeDamage(int amount)
    {
        if (invincible) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        UpdateBar();

        if (CurrentHealth <= 0)
        {
            Respawn();
        }
        else
        {
            StartCoroutine(InvincibilityFlash());
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + amount);
        UpdateBar();
    }

    public void SetRespawnPoint(Transform point)
    {
        respawnPoint = point;
    }

    public void Respawn()
    {
        StopAllCoroutines();
        invincible = false;
        if (sr != null) sr.enabled = true;

        CurrentHealth = maxHealth;
        UpdateBar();

        if (respawnPoint != null)
            transform.position = respawnPoint.position;

        rb.linearVelocity = Vector2.zero;

        if (cam != null) cam.SnapToTarget();
    }

    IEnumerator InvincibilityFlash()
    {
        invincible = true;
        float timer = 0f;
        while (timer < invincibleTime)
        {
            if (sr != null) sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }
        if (sr != null) sr.enabled = true;
        invincible = false;
    }

    void UpdateBar()
    {
        if (healthBar != null) healthBar.SetHealth(CurrentHealth, maxHealth);
    }
}