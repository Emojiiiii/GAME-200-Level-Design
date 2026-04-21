using UnityEngine;

public class PipeTimeController : MonoBehaviour
{
    public Rigidbody2D[] pipeBodies;
    public Rewindable[] rewindables;

    void Start()
    {
        if (TimeStateManager.Instance != null)
        {
            TimeStateManager.Instance.OnTimeStateChanged += HandleTimeChanged;
            Debug.Log("PipeTimeController subscribed successfully");
        }
        else
        {
            Debug.LogError("PipeTimeController: TimeStateManager.Instance is null");
        }
    }

    void OnDestroy()
    {
        if (TimeStateManager.Instance != null)
        {
            TimeStateManager.Instance.OnTimeStateChanged -= HandleTimeChanged;
        }
    }

    void HandleTimeChanged(TimeState state)
    {
        Debug.Log("PipeTimeController received state: " + state);

        if (state == TimeState.Present)
        {
            EnterPresent();
        }
        else
        {
            EnterPast();
        }
    }

    void EnterPresent()
    {
        Debug.Log("EnterPresent called");

        foreach (var rb in pipeBodies)
        {
            if (rb == null) continue;

            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Dynamic;

            Debug.Log(rb.name + " set to Dynamic");
        }
    }

    void EnterPast()
    {
        Debug.Log("EnterPast called");

        foreach (var rw in rewindables)
        {
            if (rw == null) continue;

            rw.StartReturn();
            Debug.Log(rw.name + " StartReturn called");
        }
    }
}