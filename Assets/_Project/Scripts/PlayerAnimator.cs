using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite[] idle;
    public Sprite[] run;
    public Sprite[] jump;
    public Sprite[] fall;
    public Sprite[] blast;
    public float fps = 8f;
    public float blastFps = 14f;
    public bool faceLeftByDefault = false;

    private PlayerController pc;
    private Rigidbody2D rb;
    private Sprite[] current;
    private float timer;
    private float attackTimer = -1f;

    void Awake()
    {
        pc = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void PlayAttack()
    {
        if (blast == null || blast.Length == 0) return;
        attackTimer = 0f;
    }

    void Update()
    {
        if (sr == null) return;

        bool facingLeft = pc.FacingDirection < 0f;
        sr.flipX = faceLeftByDefault ? !facingLeft : facingLeft;

        // Attack animation overrides everything while it plays
        if (attackTimer >= 0f)
        {
            int i = (int)(attackTimer * blastFps);
            if (i < blast.Length)
            {
                sr.sprite = blast[i];
                attackTimer += Time.deltaTime;
                return;
            }
            attackTimer = -1f;
            current = null;
        }

        Vector2 v = rb.linearVelocity;
        Sprite[] next;

        if (!pc.IsGrounded)
            next = (v.y > 0.1f || fall == null || fall.Length == 0) ? jump : fall;
        else if (Mathf.Abs(v.x) > 0.1f && run != null && run.Length > 0)
            next = run;
        else
            next = idle;

        if (next != current)
        {
            current = next;
            timer = 0f;
        }

        if (current == null || current.Length == 0) return;

        timer += Time.deltaTime;
        int index = (int)(timer * fps) % current.Length;
        sr.sprite = current[index];
    }
}