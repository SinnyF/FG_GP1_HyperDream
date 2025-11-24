using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    [Tooltip("Speed of the skybox rotation in degrees per second.")]
    public float rotationSpeed = 1f;

    void Update()
    {
        if (RenderSettings.skybox.HasProperty("_Rotation"))
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
        }
    }
}
