using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public int health = 4; // the amount of hits it can take before being destroyed
    public int points = 1;

    public bool isPoisoned = false;

    private void Start()
    {
        isPoisoned = false;
    }

    // function to damage the mushroom
    public void TakeDamage()
    {
        health--; // decrease the health by 1
        if (health <= 0) // if the health is 0 or less
        {
            FindObjectOfType<ScoreManager>().AddPoints(points);
            FindObjectOfType<MushroomCounter>().Remove();
            Destroy(gameObject); // destroy the current mushroom object
        }
    }

    public void Poison()
    {
        isPoisoned = true;

        // Temp way to show poison
        GetComponent<SpriteRenderer>().color = new Color(153f / 255f, 1f, 0f, 1f);
    }
}
