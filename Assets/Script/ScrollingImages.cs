using UnityEngine;
using UnityEngine.UI;

public class ScrollUIImage : MonoBehaviour
{
    public float scrollSpeed = 0.2f;
    private Material mat;
    private float offset;

    void Start()
    {
        Image image = GetComponent<Image>();

        // Duplicate the material so this instance can scroll independently
        mat = Instantiate(image.material);
        image.material = mat;
    }

    void Update()
    {
        offset = Mathf.Repeat(offset + Time.deltaTime * scrollSpeed, 1f);
        mat.SetTextureOffset("_BaseMap", new Vector2(offset, 0));
    }
}
