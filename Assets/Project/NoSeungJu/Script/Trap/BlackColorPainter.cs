using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackColorPainter : MonoBehaviour
{
    [ContextMenu("InitSprite")]
    void InitSprite()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach(SpriteRenderer renderer in spriteRenderers)
        {
            Color color = new Color();
            color = Color.black;
            color.a = 0.5f;
            renderer.color = color;
            renderer.sortingLayerName = "MiniMap";
        }
    }
}
