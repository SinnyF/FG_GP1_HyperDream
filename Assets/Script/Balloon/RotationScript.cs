using UnityEngine;

public class Rotationscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created






    // Public variables to adjust rotation from the Inspector
    public float rotationSpeedX = 0f;
    public float rotationSpeedY = 0f;
    public float rotationSpeedZ = 0f;

    void Update()
    {
        // Apply the rotation to the object
        float xRotation = rotationSpeedX * Time.deltaTime;
        float yRotation = rotationSpeedY * Time.deltaTime;
        float zRotation = rotationSpeedZ * Time.deltaTime;

        transform.Rotate(xRotation, yRotation, zRotation);

    }
}




