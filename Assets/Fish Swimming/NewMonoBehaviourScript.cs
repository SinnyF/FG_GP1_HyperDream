using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Vector3 rotation;
    private float rotationSpeed = 10f;

    private void Start()
    {
        rotation = new Vector3(1f, 1f, 1f);
    }
    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * rotationSpeed);
    }
}
   
