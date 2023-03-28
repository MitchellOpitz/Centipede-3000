using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float maxY = 10f; // max y value before bullet is destroyed

    private Rigidbody2D rb;

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
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Handle collision with enemy
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
