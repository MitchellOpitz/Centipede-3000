using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public int health = 4; // the amount of hits it can take before being destroyed
    public int points = 1;

    // function to damage the mushroom
    public void TakeDamage()
    {
        health--; // decrease the health by 1
        if (health <= 0) // if the health is 0 or less
        {
            FindObjectOfType<ScoreManager>().AddPoints(points);
            Destroy(gameObject); // destroy the current mushroom object
        }
    }
}
