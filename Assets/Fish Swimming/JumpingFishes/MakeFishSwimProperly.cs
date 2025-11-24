using UnityEngine;

public class MakeFishSwimProperly : MonoBehaviour
{
    [Header("Bobbing Settings")]
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationAngle = -15f;

    private Vector3 startPos;
    private float randomOffset;

    void Start()
    {
        startPos = transform.position;
        randomOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        // Time-based offset with individual phase
        float currentPhase = Time.time * frequency + randomOffset;

        float yOffset = Mathf.Sin(currentPhase) * amplitude;
        float yVelocity = Mathf.Cos(currentPhase) * frequency * amplitude;

        // Apply position
        transform.position = new Vector3(this.transform.position.x, yOffset, this.transform.position.z);

        // Apply tilt based on movement direction
        float tilt = -yVelocity * rotationAngle;
        transform.rotation = Quaternion.Euler(tilt, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}



