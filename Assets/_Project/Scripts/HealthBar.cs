using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform fill;

    public void SetHealth(int current, int max)
    {
        float ratio = Mathf.Clamp01((float)current / max);
        fill.localScale = new Vector3(ratio, 1f, 1f);
    }
}