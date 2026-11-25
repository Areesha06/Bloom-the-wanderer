using UnityEngine;

[DisallowMultipleComponent]
public sealed class LibrarySpiderHealth : MonoBehaviour
{
    [SerializeField, Min(1)] private int health = 1;
    [SerializeField] private AnimationClip deathClip;

    private static readonly int DeathState = Animator.StringToHash("Base Layer.Spider_Death");

    private bool dying;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (dying || !other.TryGetComponent(out Projectile projectile) ||
            projectile.damagesPlayer || projectile.damage <= 0)
            return;

        Destroy(projectile.gameObject);
        health -= projectile.damage;
        if (health > 0)
            return;

        dying = true;
        foreach (Collider2D hitbox in GetComponentsInChildren<Collider2D>())
            hitbox.enabled = false;

        if (deathClip == null || !TryGetComponent(out Animator animator) ||
            !animator.isActiveAndEnabled || animator.runtimeAnimatorController == null ||
            !animator.HasState(0, DeathState))
        {
            Destroy(gameObject);
            return;
        }

        animator.Play(DeathState, 0, 0f);
        Destroy(gameObject, deathClip.length + 0.05f);
    }
}
