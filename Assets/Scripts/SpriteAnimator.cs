using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    public bool playOnce;

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
                if (playOnce)
                {
                    Destroy(gameObject);
                }
                currentSpriteIndex = 0;
            }
            spriteRenderer.sprite = sprites[currentSpriteIndex];
            frameCounter = 0;
        }
    }
}
