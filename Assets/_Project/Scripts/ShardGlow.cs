using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightPulse : MonoBehaviour
{
    public float minIntensity = 1.8f;
    public float maxIntensity = 3.2f;
    public float pulseSpeed = 3f;

    private Light2D light2D;

    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    void Update()
    {
        if (light2D != null)
        {
            //intensity change between min and max
            float wave = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;
            light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity, wave);
        }
    }
}