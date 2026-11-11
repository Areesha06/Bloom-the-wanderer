using UnityEngine;

public class MinionChase : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private GameObject platform;

    private Rigidbody2D rb;
    private Collider2D platformCollider;
    private Collider2D enemyCollider;
    private float directionX = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        platformCollider = platform.GetComponent<Collider2D>();
        
    }

    void FixedUpdate()
    {
        Bounds platformBounds = platformCollider.bounds;
        float enemyHalfWidth = enemyCollider != null ? enemyCollider.bounds.extents.x : 0f;
        float minX = platformBounds.min.x + enemyHalfWidth;
        float maxX = platformBounds.max.x - enemyHalfWidth;

        if (minX > maxX)
        {
            minX = maxX = platformBounds.center.x;
        }

        if (Mathf.Approximately(minX, maxX))
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        float nextX = rb.position.x + directionX * speed * Time.fixedDeltaTime;
        if (nextX < minX)
        {
            directionX = 1f;
            rb.position = new Vector2(minX, rb.position.y);
        }
        else if (nextX > maxX)
        {
            directionX = -1f;
            rb.position = new Vector2(maxX, rb.position.y);
        }

        rb.linearVelocity = new Vector2(directionX * speed, rb.linearVelocity.y);

        if (directionX > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
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