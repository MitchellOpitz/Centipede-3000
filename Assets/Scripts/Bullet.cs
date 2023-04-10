using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float maxY = 10f; // max y value before bullet is destroyed

    private Rigidbody2D rb;
    private bool hasHitEnemy = false; // added variable to track if bullet has already hit an enemy

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        // check if bullet is beyond maxY value
        if (transform.position.y > maxY)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasHitEnemy) // check if bullet has already hit an enemy
        {
            if (other.gameObject.CompareTag("Spider"))
            {
                // Handle collision with enemy
                Destroy(gameObject);
                other.gameObject.GetComponent<Spider>().TakeDamage();
                hasHitEnemy = true; // set the variable to true since bullet has hit an enemy
            }

            if (other.gameObject.CompareTag("Centipede"))
            {
                // Handle collision with enemy
                Destroy(gameObject);
                other.gameObject.GetComponent<CentipedeSegment>().TakeDamage();
                hasHitEnemy = true; // set the variable to true since bullet has hit an enemy
            }

            if (other.gameObject.CompareTag("Scorpion"))
            {
                // Handle collision with enemy
                other.gameObject.GetComponent<Scorpion>().TakeDamage();
                Destroy(gameObject);
                hasHitEnemy = true; // set the variable to true since bullet has hit an enemy
            }

            if (other.gameObject.CompareTag("Flea"))
            {
                // Handle collision with enemy
                other.gameObject.GetComponent<Flea>().TakeDamage();
                Destroy(gameObject);
                hasHitEnemy = true; // set the variable to true since bullet has hit an enemy
            }

            if (other.gameObject.CompareTag("Mushroom"))
            {
                other.GetComponent<Mushroom>().TakeDamage();
                Destroy(gameObject);
                hasHitEnemy = true; // set the variable to true since bullet has hit an enemy
            }
        }
    }
}
