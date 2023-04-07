using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public Sprite[] mushroomSprites;
    public Sprite[] poisonMushroomSprites;
    public int health = 4; // the amount of hits it can take before being destroyed
    public int points = 1;

    public bool isPoisoned = false;

    private int currentSpriteIndex = 0;
    private SpriteRenderer spriteRenderer;

    public ParticleSystem redParticles;
    public ParticleSystem greenParticles;
    public ParticleSystem whiteParticles;

    private void Start()
    {
        isPoisoned = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (Mathf.Abs(transform.position.y) > 9 || Mathf.Abs(transform.position.x) > 20)
        {
            Destroy(gameObject);
        }
    }

    // function to damage the mushroom
    public void TakeDamage()
    {
        health--; // decrease the health by 1
        if (isPoisoned)
        {
            FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(greenParticles, transform.position);
            FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(whiteParticles, transform.position);
        } else
        {
            FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(redParticles, transform.position);
            FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(greenParticles, transform.position);
        }

        if (health <= 0) // if the health is 0 or less
        {
            FindObjectOfType<ScoreManager>().AddPoints(points);
            GetComponent<ScoreDisplay>().CallScore();
            Destroy(gameObject); // destroy the current mushroom object
        } else
        {
            currentSpriteIndex++;
            if (isPoisoned)
            {
                spriteRenderer.sprite = poisonMushroomSprites[currentSpriteIndex];
            } else
            {
                spriteRenderer.sprite = mushroomSprites[currentSpriteIndex];
            }
        }
    }

    public void Poison()
    {
        isPoisoned = true;

        spriteRenderer.sprite = poisonMushroomSprites[currentSpriteIndex];
    }
}
