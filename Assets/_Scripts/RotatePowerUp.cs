using UnityEngine;

public class RotatePowerUp : MonoBehaviour
{

    [SerializeField]private float rotationSpeed = 10.0f;
    [SerializeField] private float yOffset = 1f;

    private void Start()
    {

    }

    private void Update()
    {
        TransformObject();
    }

    private void TransformObject()
    {
        transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotationSpeed, Space.Self);
        transform.position = new Vector3(gameObject.transform.position.x, (Mathf.Sin(Time.time) + yOffset) * 0.2f, gameObject.transform.position.z);
    }
}
