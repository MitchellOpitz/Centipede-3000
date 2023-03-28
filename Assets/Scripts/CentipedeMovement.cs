using UnityEngine;
using System.Collections;

public class CentipedeMovement : MonoBehaviour
{

    public float speed = 2.0f; // speed of movement
    public float maxX = 10.0f; // maximum X position on screen
    public float minX = -10.0f; // minimum X position on screen
    public float yOffset = -1.0f; // amount to move down when reaching the edge

    private Vector3 direction = Vector3.right; // current direction of movement

    // Update is called once per frame
    void Update()
    {
        // move the centipede
        transform.position += direction * speed * Time.deltaTime;

        // check if we reached the edge of the screen
        if (transform.position.x >= maxX || transform.position.x <= minX)
        {
            // move down and change direction
            direction = new Vector3(direction.x * -1, 0, 0);
            transform.position += new Vector3(0, yOffset, 0);
        }
    }
}
