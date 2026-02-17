using UnityEngine;

public class TimeStoppable2D : MonoBehaviour
{
    [Header("Ignore (for Player)")]
    public bool immune = false;

    [Header("Also pause these (optional)")]
    public bool pauseAnimator = true;
    public bool pauseParticles = true;

    Rigidbody2D rb;
    Animator anim;
    ParticleSystem[] particles;

    Vector2 savedVel;
    float savedAngVel;
    RigidbodyConstraints2D savedConstraints;

    float savedAnimSpeed;
    bool hasRb, hasAnim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        particles = GetComponentsInChildren<ParticleSystem>(true);

        hasRb = rb != null;
        hasAnim = anim != null;
    }

    void OnEnable()
    {
        TimePauseManager.OnPauseChanged += HandlePause;
    }

    void OnDisable()
    {
        TimePauseManager.OnPauseChanged -= HandlePause;
    }

    void HandlePause(bool paused)
    {
        if (immune) return;

        if (paused)
        {
            if (hasRb)
            {
                savedVel = rb.velocity;
                savedAngVel = rb.angularVelocity;
                savedConstraints = rb.constraints;

                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            if (pauseAnimator && hasAnim)
            {
                savedAnimSpeed = anim.speed;
                anim.speed = 0f;
            }

            if (pauseParticles && particles != null)
            {
                foreach (var p in particles) p.Pause(true);
            }
        }
        else
        {
            if (hasRb)
            {
                rb.constraints = savedConstraints;
                rb.velocity = savedVel;
                rb.angularVelocity = savedAngVel;
            }

            if (pauseAnimator && hasAnim)
            {
                anim.speed = savedAnimSpeed;
            }

            if (pauseParticles && particles != null)
            {
                foreach (var p in particles) p.Play(true);
            }
        }
    }
}