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
            renderer.color = Color.black;
        }
    }
}
