using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flea : MonoBehaviour
{
    public float speed = 2f; // Speed of the flea
    public float yThreshold = -5f; // Y value below which the flea is destroyed
    public float mushroomSpawnChance = 0.5f; // Chance of spawning a mushroom at each grid point
    public float maxMushroomY = 10f;
    public float minMushroomY = -10f;
    public int points = 200;

    public GameObject mushroomPrefab;

    public ParticleSystem redParticles;
    public ParticleSystem greenParticles;
    public ParticleSystem whiteParticles;

    private Vector2 targetPosition;
    private Vector2 direction = Vector2.down;
    private SFXManager sfxManager;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = GridPosition(transform.position);
        targetPosition.y += direction.y;
        sfxManager = FindObjectOfType<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position;
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, step);

        if (Vector2.Distance(currentPosition, targetPosition) < 0.1f)
        {
            // Flea has reached a grid point, check if a mushroom should be spawned
            if (ShouldSpawnMushroom())
            {
                SpawnMushroom(targetPosition);
            }

            targetPosition = GridPosition(transform.position);
            targetPosition.y += direction.y;

            // Destroy the flea if it has passed below the y threshold
            if (transform.position.y < yThreshold)
            {
                Destroy(gameObject);
            }
        }
    }

    private bool ShouldSpawnMushroom()
    {
        // Check if a mushroom already exists at the target grid point
        Collider2D collider = Physics2D.OverlapBox(targetPosition, Vector2.zero, 0f);
        if (collider != null && collider.GetComponent<Mushroom>() != null)
        {
            return false;
        }

        // Generate a random number between 0 and 1 and compare it to the mushroom spawn chance
        float random = Random.Range(0f, 1f);
        return random < mushroomSpawnChance;
    }

    private void SpawnMushroom(Vector2 position)
    {
        if (position.y > minMushroomY && position.y < maxMushroomY)
        {
            // Instantiate a new mushroom at the target position
            Instantiate(mushroomPrefab, position, Quaternion.identity);
            FindObjectOfType<MushroomCounter>().Add();
        }
    }

    private Vector2 GridPosition(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }

    public void TakeDamage()
    {
        FindObjectOfType<ScoreManager>().AddPoints(points);
        FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(redParticles, transform.position);
        FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(greenParticles, transform.position);
        FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(whiteParticles, transform.position);
        FindObjectOfType<CameraShake>().Shake();
        GetComponent<ScoreDisplay>().CallScore();
        sfxManager.Play("Splat");
        Destroy(gameObject); // destroy the current mushroom object
    }
}
