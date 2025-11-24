using System.Collections.Generic;
using UnityEngine;

public class PaletteManager : MonoBehaviour
{
    public List<Color> palette = new List<Color>();

    private void OnValidate()
    {
        if (palette != null && palette.Count > 0)
        {
            UpdateTexture();
        }
    }

    private void UpdateTexture()
    {
        var tex2D = GetTextureFromPalette();
        Shader.SetGlobalTexture("_TexPalette", tex2D);
    }

    private Texture2D GetTextureFromPalette()
    {
        var colors = palette;
        var texture2D = new Texture2D(colors.Count, 1)
        {
            filterMode = FilterMode.Bilinear,
            wrapMode = TextureWrapMode.Clamp,
        };

        for (int i = 0; i < colors.Count; i++)
        {
            texture2D.SetPixel(i, 0, colors[i]);
        }

        texture2D.Apply();

        return texture2D;
    }
}
