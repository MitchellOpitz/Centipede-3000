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

    private Rigidbody2D rb;
    private SFXManager sfxManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sfxManager = FindObjectOfType<SFXManager>();
        speed *= FindObjectOfType<PlayerManager>().GetMultiplier();
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
        Destroy(gameObject); // destroy the current mushroom object
    }
}
