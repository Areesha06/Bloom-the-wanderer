using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 12f;
    public int damage = 1;
    public bool damagesPlayer = false;
    public float lifetime = 3f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Hit a wall or ground
        if ((groundLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(gameObject);
            return;
        }

        if (damagesPlayer)
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }

            Cobweb web = other.GetComponent<Cobweb>();
            if (web != null)
            {
                web.TakeDamage(damage);
                Destroy(gameObject);
            }

            string hitName = other.gameObject.name.ToLower();
            if (other.CompareTag("Enemy") || hitName.Contains("SkullGoon") || hitName.Contains("BossShield") || hitName.Contains("BossKing") || hitName.Contains("king"))
            {
                // Send TakeDamage message to whoever was hit (EnemyHealth or BossController)
                other.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                        
                // Destroy this projectile on impact so it does not pass through
                Destroy(gameObject);
                return;
            }
        }
    }
}