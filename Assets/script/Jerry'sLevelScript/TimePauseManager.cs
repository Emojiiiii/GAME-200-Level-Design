using System;
using UnityEngine;

public class TimePauseManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; }
    public static event Action<bool> OnPauseChanged;

    public KeyCode pauseKey = KeyCode.Q;

    private static TimePauseManager instance;

    void Awake()
    {
        // 防止重复实例导致一按变 True 又变 False
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            IsPaused = !IsPaused;
            // Debug.Log("Time Pause = " + IsPaused);
            OnPauseChanged?.Invoke(IsPaused);
        }
    }
}
