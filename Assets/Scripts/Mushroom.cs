using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public Sprite[] mushroomSprites;
    public int health = 4; // the amount of hits it can take before being destroyed
    public int points = 1;

    public bool isPoisoned = false;

    private int currentSpriteIndex = 0;
    private SpriteRenderer spriteRenderer;

    public ParticleSystem redParticles;
    public ParticleSystem greenParticles;

    private void Start()
    {
        isPoisoned = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // function to damage the mushroom
    public void TakeDamage()
    {
        health--; // decrease the health by 1
        FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(redParticles, transform.position);
        FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(greenParticles, transform.position);

        if (health <= 0) // if the health is 0 or less
        {
            FindObjectOfType<ScoreManager>().AddPoints(points);
            FindObjectOfType<MushroomCounter>().Remove();
            Destroy(gameObject); // destroy the current mushroom object
        } else
        {
            currentSpriteIndex++;
            spriteRenderer.sprite = mushroomSprites[currentSpriteIndex];
        }
    }

    public void Poison()
    {
        isPoisoned = true;

        // Temp way to show poison
        GetComponent<SpriteRenderer>().color = new Color(153f / 255f, 1f, 0f, 1f);
    }
}
