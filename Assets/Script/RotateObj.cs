using UnityEngine;

public class RotateObj : MonoBehaviour
{
    private Vector3 rotation;
    [SerializeField] private float rotationSpeed = 10f;

    private void Start()
    {
        rotation = new Vector3(0f, 0f, -1f);
    }
    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * rotationSpeed);
    }
}
