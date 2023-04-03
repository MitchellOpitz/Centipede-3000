using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float minX, maxX, minY, maxY;
    public float overlapRadius = 1f;
    public Vector3 overlapOffset = Vector3.zero;

    private bool isColliding;

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0f);

        if (isColliding)
        {
            // Calculate a new direction vector that avoids the mushrooms
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + overlapOffset, overlapRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Mushroom"))
                {
                    Vector3 avoidDir = (transform.position - collider.transform.position).normalized;
                    direction += avoidDir;
                }
            }
        }

        transform.position += direction.normalized * speed * Time.deltaTime;

        // Clamp player's position within user-specified area
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Mushroom"))
        {
            isColliding = true;
        }

        if (other.gameObject.CompareTag("Centipede"))
        {
            FindObjectOfType<PlayerManager>().LoseLife();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Mushroom"))
        {
            isColliding = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + overlapOffset, overlapRadius);
    }


}
