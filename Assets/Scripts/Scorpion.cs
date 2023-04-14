using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpion : MonoBehaviour
{
    public float speed = 10f;
    public float maxX = 10f;
    public int points = 1000;

    public ParticleSystem redParticles;
    public ParticleSystem whiteParticles;
    public GameObject largeExplosion;

    private Rigidbody2D rb;
    private SFXManager sfxManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sfxManager = FindObjectOfType<SFXManager>();
        speed *= FindObjectOfType<PlayerManager>().GetMultiplier();
        transform.position = GridPosition(transform.position);
    }

    private void Update()
    {
        if (transform.position.x > maxX)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mushroom"))
        {
            Mushroom mushroom = collision.GetComponent<Mushroom>();
            mushroom.Poison();
        }
        else if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage()
    {
        FindObjectOfType<ScoreManager>().AddPoints(points);
        FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(redParticles, transform.position);
        FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(whiteParticles, transform.position);
        FindObjectOfType<CameraShake>().Shake();
        GetComponent<ScoreDisplay>().CallScore();
        sfxManager.Play("Splat");
        Instantiate(largeExplosion, transform.position, Quaternion.identity);
        FindObjectOfType<Achievements>().UpdateScorpions();
        Destroy(gameObject); // destroy the current mushroom object
    }
    private Vector2 GridPosition(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }
}
