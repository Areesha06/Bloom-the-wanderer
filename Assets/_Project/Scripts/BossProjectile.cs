using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 2;
    private Vector2 moveDirection = Vector2.left;

    void Start()
    {
        Destroy(gameObject, 4f);
    }

    public void Launch(Vector2 dir)
    {
        moveDirection = dir.normalized;
        if (dir.x > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void Update()
    {
        //king moves
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("King") || other.name.Contains("Shield") || other.name.Contains("SkullGoon"))
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            other.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
            return;
        }

        if (other.name.Contains("Wall") || other.name.Contains("Ground") || other.name.Contains("Platform"))
        {
            Destroy(gameObject);
        }
    }
}