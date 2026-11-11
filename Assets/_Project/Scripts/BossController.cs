using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Stats & Settings")]
    public int health = 100;
    public GameObject shield;
    public GameObject platform; 
    public BossArenaManager arenaManager;

    [Header("Movement")]
    public float moveSpeed = 2.5f;
    public float stopDistance = 2.5f;

    [Header("Attack")]
    public GameObject projectilePrefab;
    public float attackRate = 3f;

    private Transform player;
    private Rigidbody2D rb;
    private Collider2D platformCollider;
    private Collider2D bossCollider;
    private SpriteRenderer spriteRenderer;
    private float attackTimer = 0f;
    private bool wasPlayerOnPlatform = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bossCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (platform != null)
        {
            platformCollider = platform.GetComponent<Collider2D>();
        }

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        if (shield != null)
        {
            shield.SetActive(true);
        }
    }

    public void StartBossBattle()
    {
    }

    void Update()
    {
        GameObject minionLeft = GameObject.Find("SkullGoon(Clone)");
        if (minionLeft == null) minionLeft = GameObject.Find("SkullGoon");
        if (minionLeft == null) minionLeft = GameObject.FindWithTag("Enemy");

        if (shield != null)
        {
            shield.SetActive(minionLeft != null);
        }

       
        bool isPlayerOnPlatform = IsPlayerOnMyPlatform();

        if (isPlayerOnPlatform)
        {
            if (!wasPlayerOnPlatform)
            {
                attackTimer = 0f;
                Shoot();
            }
            else
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackRate)
                {
                    attackTimer = 0f;
                    Shoot();
                }
            }
        }
        else
        {
            attackTimer = 0f;
        }

        wasPlayerOnPlatform = isPlayerOnPlatform;
    }

    void FixedUpdate()
    {
        if (player == null || platformCollider == null || rb == null) return;
        if (!IsPlayerOnMyPlatform()) return;

        float playerOffsetX = player.position.x - rb.position.x;

        //flip king's direction
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = (playerOffsetX < 0f);
        }

        Bounds bounds = platformCollider.bounds;
        float halfWidth = bossCollider != null ? bossCollider.bounds.extents.x : 0.5f;
        float minX = bounds.min.x + halfWidth;
        float maxX = bounds.max.x - halfWidth;

        float targetX = rb.position.x;
        if (Mathf.Abs(playerOffsetX) > stopDistance)
        {
            float direction = Mathf.Sign(playerOffsetX);
            targetX = Mathf.Clamp(player.position.x - (direction * stopDistance), minX, maxX);
        }

        float nextX = Mathf.MoveTowards(rb.position.x, targetX, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(new Vector2(nextX, rb.position.y));
    }

    bool IsPlayerOnMyPlatform()
    {
        if (player == null || platformCollider == null) return false;

        //player on platform check
        Bounds bounds = platformCollider.bounds;
        bool withinX = player.position.x >= bounds.min.x && player.position.x <= bounds.max.x;
        bool onTopY = player.position.y >= (bounds.max.y - 0.5f) && player.position.y <= (bounds.max.y + 2.5f);

        return withinX && onTopY;
    }

    void Shoot()
    {
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.Play("KingAttack");
        }

        if (projectilePrefab != null && player != null)
        {
            float shootDirX = (player.position.x > transform.position.x) ? 1f : -1f;
            Vector3 spawnPos = transform.position + new Vector3(shootDirX * 1.2f, 0f, 0f);
            GameObject fireball = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

            BossProjectile projScript = fireball.GetComponent<BossProjectile>();
            if (projScript != null)
            {
                projScript.Launch(new Vector2(shootDirX, 0f));
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (shield != null && shield.activeSelf)
        {
            return;
        }

        health -= 25;
        if (health <= 0)
        {
            if (arenaManager != null)
            {
                arenaManager.OnBossDefeated();
            }
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("TakeDamage", 1, SendMessageOptions.DontRequireReceiver);
        }
    }
}