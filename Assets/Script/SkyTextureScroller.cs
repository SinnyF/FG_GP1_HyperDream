using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SkyTextureScroller : MonoBehaviour
{
    [Tooltip("Scroll speed on the X axis (horizontal).")]
    public float scrollSpeedX = 0.02f;

    [Tooltip("Scroll speed on the Y axis (vertical).")]
    public float scrollSpeedY = 0.01f;

    private MeshRenderer meshRenderer;
    private Material skyMaterial;
    private Vector2 offset;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        // Get the instance material directly from the component
        // (modifies only this object's material, not the shared asset)
        skyMaterial = meshRenderer.material;
    }

    void Update()
    {
        if (skyMaterial == null) return;

        offset.x += scrollSpeedX * Time.deltaTime;
        offset.y += scrollSpeedY * Time.deltaTime;

        // Keep values in range to avoid overflow
        offset.x %= 1f;
        offset.y %= 1f;

        // Apply the offset to the main texture (for URP/Lit use "_BaseMap")
        if (skyMaterial.HasProperty("_MainTex"))
            skyMaterial.SetTextureOffset("_MainTex", offset);
    }
}