using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;

    private int frameCounter = 0;
    private int currentSpriteIndex = 0;

    private void Update()
    {
        frameCounter++;

        if (frameCounter >= 6)
        {
            currentSpriteIndex++;
            if (currentSpriteIndex >= sprites.Length)
            {
                currentSpriteIndex = 0;
            }
            spriteRenderer.sprite = sprites[currentSpriteIndex];
            frameCounter = 0;
        }
    }
}
