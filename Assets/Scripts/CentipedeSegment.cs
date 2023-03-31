using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeSegment : MonoBehaviour
{

    public SpriteRenderer spriteRenderer { get; private set; }
    public Centipede centipede { get; set; }
    public CentipedeSegment ahead { get; set; }
    public CentipedeSegment behind { get; set; }
    public bool isHead => ahead == null;

    private Vector2 targetPosition;
    private Vector2 direction = Vector2.left + Vector2.down;

    private bool isPoisoned = false;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (isHead && Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            UpdateHeadSegment();
        }

        Vector2 currentPosition = transform.position;
        float speed = centipede.speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed);

        Vector2 movementDirection = (targetPosition - currentPosition).normalized;
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void UpdateHeadSegment()
    {

        Vector2 gridPosition = GridPosition(transform.position);
        targetPosition = gridPosition;
        targetPosition.x += direction.x;

        if (Physics2D.OverlapBox(targetPosition, Vector2.zero, 0f, centipede.collisionMask))
        {
            Mushroom mushroom = Physics2D.OverlapBoxAll(targetPosition, Vector2.zero, 0f, centipede.collisionMask)[0].gameObject.GetComponent<Mushroom>();

            if (mushroom != null && mushroom.isPoisoned)
            {
                isPoisoned = true;
                direction.y = -1f;
                Debug.Log("Head poisoned.");

            } else
            {
                direction.x = -direction.x;
                targetPosition.x = gridPosition.x;
                targetPosition.y = gridPosition.y + direction.y;

                Bounds homeBounds = centipede.homeArea.bounds;

                if ((direction.y == 1f && targetPosition.y > homeBounds.max.y) ||
                    (direction.y == -1f && targetPosition.y < homeBounds.min.y))
                {
                    direction.y = -direction.y;
                    targetPosition.y = gridPosition.y + direction.y;
                }
            }
        }

        if (isPoisoned)
        {
            targetPosition.x = gridPosition.x;
            targetPosition.y = gridPosition.y + direction.y;

            Bounds homeBounds = centipede.homeArea.bounds;
            if (targetPosition.y < homeBounds.min.y)
            {
                Debug.Log("Reached bottom.");
                Debug.Log("Head Not Poisoned.");
                isPoisoned = false;
            }
        }

        if (behind != null)
        {
            behind.UpdateBodySegment();
        }
    }

    private void UpdateBodySegment()
    {

        targetPosition = GridPosition(ahead.transform.position);
        direction = ahead.direction;

        if (ahead != null && ahead.isPoisoned)
        {
            isPoisoned = true;
            Debug.Log("Segment Poisoned.");
        }
        else
        {
            isPoisoned = false;
            Debug.Log("Segment Not Poisoned.");
        }

        if (behind != null)
        {
            behind.UpdateBodySegment();
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
        centipede.Remove(this);
    }
}
