using UnityEngine;

public class UIPulseAndOscillate_Ranged : MonoBehaviour
{
    [Header("Pulse Range")]
    public float minScale = 0.9f;     // smallest size
    public float maxScale = 1.1f;     // biggest size
    public float pulseSpeed = 2f;     // how fast it pulses

    [Header("Rotation Range")]
    public float minAngle = -10f;     // min Z rotation
    public float maxAngle = 10f;      // max Z rotation
    public float rotationSpeed = 2f;  // how fast it rotates

    public bool playOnStart = true;

    private Vector3 baseScale;
    private bool animating;

    void Start()
    {
        baseScale = transform.localScale;
        if (playOnStart)
            StartAnimation();
    }

    void Update()
    {
        if (!animating) return;

        // ---- SIN THAT MOVES FROM 0 → 1 ----
        float tPulse = (Mathf.Sin(Time.unscaledTime * pulseSpeed) + 1f) * 0.5f;
        float tRot = (Mathf.Sin(Time.unscaledTime * rotationSpeed) + 1f) * 0.5f;

        // ---- SCALE BETWEEN MIN AND MAX ----
        float scale = Mathf.Lerp(minScale, maxScale, tPulse);
        transform.localScale = baseScale * scale;

        // ---- ROTATE BETWEEN MIN AND MAX ANGLES ----
        float angle = Mathf.Lerp(minAngle, maxAngle, tRot);
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void StartAnimation() => animating = true;

    public void StopAnimation()
    {
        animating = false;
        transform.localScale = baseScale;
        transform.localRotation = Quaternion.identity;
    }
}
