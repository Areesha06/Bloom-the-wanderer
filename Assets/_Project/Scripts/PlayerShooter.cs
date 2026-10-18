using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    public Projectile projectilePrefab;
    public float cooldown = 0.35f;

    private PlayerController pc;
    private float nextFire;
    private PlayerAnimator anim;

    void Awake()
    {
        pc = GetComponent<PlayerController>();
        anim = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null || projectilePrefab == null) return;
        if (pc.inputLocked) return;

        bool firePressed = kb.leftCtrlKey.wasPressedThisFrame
                        || kb.rightCtrlKey.wasPressedThisFrame
                        || kb.fKey.wasPressedThisFrame;   // F = backup key

        if (firePressed && Time.time >= nextFire)
        {
            nextFire = Time.time + cooldown;
            Shoot();
        }
    }

    void Shoot()
    {
        float dir = pc.FacingDirection;
        Vector3 spawnPos = transform.position + new Vector3(0.7f * dir, 0f, 0f);
        Projectile p = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        p.Launch(new Vector2(dir, 0f));
        if (anim != null) anim.PlayAttack();
    }
}