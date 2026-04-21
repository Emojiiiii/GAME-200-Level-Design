using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RandomLightFlicker2D : MonoBehaviour
{
    public Light2D targetLight;

    [Header("Intensity Range")]
    public float minIntensity = 0.2f;
    public float maxIntensity = 1.0f;

    [Header("Flicker Timing")]
    public float minInterval = 0.03f;
    public float maxInterval = 0.12f;

    private float timer;

    void Reset()
    {
        targetLight = GetComponent<Light2D>();
    }

    void Start()
    {
        SetNextFlickerTime();
    }

    void Update()
    {
        if (targetLight == null) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            targetLight.intensity = Random.Range(minIntensity, maxIntensity);
            SetNextFlickerTime();
        }
    }

    void SetNextFlickerTime()
    {
        timer = Random.Range(minInterval, maxInterval);
    }
}
