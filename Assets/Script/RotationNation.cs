using UnityEngine;

public class UIRotateForever : MonoBehaviour
{
    [Tooltip("Degrees per second (positive = clockwise, negative = counterclockwise)")]
    public float rotationSpeed = 90f;

    [Tooltip("Start rotating on play?")]
    public bool playOnStart = true;

    private bool rotating;

    void Start()
    {
        if (playOnStart)
            StartRotation();
    }

    void Update()
    {
        if (!rotating) return;

        // Uses unscaled delta time so it rotates even when timeScale = 0
        float rot = rotationSpeed * Time.unscaledDeltaTime;
        transform.Rotate(0f, 0f, rot);
    }

    public void StartRotation() => rotating = true;

    public void StopRotation() => rotating = false;
}
