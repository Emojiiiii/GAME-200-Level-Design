using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TimeStateManager : MonoBehaviour
{
    public static TimeStateManager Instance;

    public TimeState CurrentState { get; private set; } = TimeState.Present;
    public float switchInterval = 15f;
    private float timer;

    [Header("Train")]
    public GameObject[] objectsVisibleInPast;
    public GameObject[] objectsVisibleInPresent;

    [Header("2D Lights")]
    public Light2D[] controlledLights;
    public float pastLightIntensity = 1f;
    public float presentLightIntensity = 0f;

    public System.Action<TimeState> OnTimeStateChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ApplyState(CurrentState);
        OnTimeStateChanged?.Invoke(CurrentState);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchInterval)
        {
            timer = 0f;
            SwitchState();
        }
    }

    public void SwitchState()
    {
        CurrentState = CurrentState == TimeState.Past ? TimeState.Present : TimeState.Past;

        Debug.Log("Time State Switched To: " + CurrentState);

        ApplyState(CurrentState);
        OnTimeStateChanged?.Invoke(CurrentState);
    }

    void ApplyState(TimeState state)
    {
        bool isPast = state == TimeState.Past;

        // 控制过去出现的物体
        if (objectsVisibleInPast != null)
        {
            foreach (GameObject obj in objectsVisibleInPast)
            {
                if (obj != null)
                    obj.SetActive(isPast);
            }
        }

        // 控制现在出现的物体
        if (objectsVisibleInPresent != null)
        {
            foreach (GameObject obj in objectsVisibleInPresent)
            {
                if (obj != null)
                    obj.SetActive(!isPast);
            }
        }

        // 控制同一组 Light2D 的亮度
        if (controlledLights != null)
        {
            float targetIntensity = isPast ? pastLightIntensity : presentLightIntensity;

            foreach (Light2D light2D in controlledLights)
            {
                if (light2D != null)
                    light2D.intensity = targetIntensity;
            }
        }
    }
}