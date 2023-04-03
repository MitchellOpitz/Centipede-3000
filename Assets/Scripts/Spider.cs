using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public float speed = 1f;
    public float minY, maxY;
    public float minX, maxX;

    public ParticleSystem redParticles;
    public ParticleSystem greenParticles;
    public ParticleSystem whiteParticles;

    private Vector2 currentTarget;
    private int xDirection;
    private bool isMovingRight;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.value < 0.5f)
        {
            transform.position = new Vector2(minX, Random.Range(minY, maxY));
            SetTarget();
            isMovingRight = true;
        }
        else
        {
            transform.position = new Vector2(maxX, Random.Range(minY, maxY));
            SetTarget();
            isMovingRight = false;
        }

        xDirection = isMovingRight ? 1 : -1;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, currentTarget);
        if (distance < 0.1f)
        {
            SetTarget();
        }

        currentTarget = GridPosition(currentTarget);
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        if (transform.position.x < minX || transform.position.x > maxX)
        {
            Destroy(gameObject);
        }
    }

    private Vector2 GridPosition(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }

    private void SetTarget()
    {
        if (Random.value < 0.5f)
        {
            // Move vertically
            currentTarget = new Vector2(transform.position.x, Random.Range(minY, maxY));
        }
        else
        {
            var number = Mathf.FloorToInt(Random.Range(1, 6));
            currentTarget = transform.position + new Vector3(number * xDirection, number, 0);
        }

        if (currentTarget.y > maxY || currentTarget.y < minY)
        {
            SetTarget();
        }
    }

    public void TakeDamage()
    {
        var score = GetComponent<ScoreDisplay>();
        if(transform.position.y > -3)
        {
            score.UpdateScore(300);
            FindObjectOfType<ScoreManager>().AddPoints(300);
        } else if (transform.position.y > -6)
        {
            score.UpdateScore(600);
            FindObjectOfType<ScoreManager>().AddPoints(600);
        } else
        {
            score.UpdateScore(900);
            FindObjectOfType<ScoreManager>().AddPoints(900);
        }

        GetComponent<ScoreDisplay>().CallScore();
        FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(redParticles, transform.position);
        FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(greenParticles, transform.position);
        FindObjectOfType<ParticleEffectsManager>().PlayParticleSystem(whiteParticles, transform.position);
        FindObjectOfType<CameraShake>().Shake();
        Destroy(gameObject);
    }
}
