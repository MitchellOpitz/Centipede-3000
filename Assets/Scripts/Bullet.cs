using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
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
