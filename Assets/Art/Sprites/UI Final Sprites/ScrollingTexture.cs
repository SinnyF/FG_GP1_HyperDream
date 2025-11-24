using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ScrollingTexture : MonoBehaviour
{
    [Tooltip("Scroll speed on the X axis (positive = right, negative = left)")]
    public float scrollSpeedX = 0.5f;

    [Tooltip("Scroll speed on the Y axis (usually 0 for horizontal scroll)")]
    public float scrollSpeedY = 0f;

    private Material runtimeMaterial;
    private Vector2 offset;

    void Awake()
    {
        // Get the Image component
        Image image = GetComponent<Image>();

        // Create a runtime copy of the material so it doesn’t affect others
        runtimeMaterial = Instantiate(image.material);
        image.material = runtimeMaterial;
    }

    void Update()
    {
        // Update texture offset based on time and speed
        offset.x += scrollSpeedX * Time.unscaledDeltaTime;
        offset.y += scrollSpeedY * Time.unscaledDeltaTime;

        // Apply offset
        runtimeMaterial.mainTextureOffset = offset;
    }

    void OnDestroy()
    {
        // Clean up to prevent memory leaks
        if (runtimeMaterial != null)
            Destroy(runtimeMaterial);
    }
}