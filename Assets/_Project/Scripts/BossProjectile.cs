using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 2;
    private Vector2 moveDirection = Vector2.left;

    void Start()
    {
        // Delete self after 4 seconds
        Destroy(gameObject, 4f);
    }

    public void Launch(Vector2 dir)
    {
        moveDirection = dir.normalized;

        // Flip fireball sprite horizontally if shooting right
        if (dir.x > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void Update()
    {
        // Move horizontally
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore boss, shield, and minions
        if (other.name.Contains("King") || other.name.Contains("Shield") || other.name.Contains("SkullGoon"))
        {
            return;
        }

        // Damage the player
        if (other.CompareTag("Player"))
        {
            other.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
            return;
        }

        // Destroy when hitting walls, platforms, or ground
        if (other.name.Contains("Wall") || other.name.Contains("Ground") || other.name.Contains("Platform"))
        {
            Destroy(gameObject);
        }
    }
}