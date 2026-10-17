using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.15f;
    public Vector2 offset = new Vector2(0f, 1.5f);

    [Header("Optional limits (tick to use)")]
    public bool useLimits = false;
    public float minX = -100f, maxX = 100f;
    public float minY = -100f, maxY = 100f;

    private Vector3 velocity;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = new Vector3(target.position.x + offset.x,
                                      target.position.y + offset.y,
                                      transform.position.z);

        if (useLimits)
        {
            desired.x = Mathf.Clamp(desired.x, minX, maxX);
            desired.y = Mathf.Clamp(desired.y, minY, maxY);
        }

        transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
    }

    // Use this later when moving the player to a new area
    public void SnapToTarget()
    {
        if (target == null) return;
        transform.position = new Vector3(target.position.x + offset.x,
                                         target.position.y + offset.y,
                                         transform.position.z);
        velocity = Vector3.zero;
    }
}