using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public float horizontalSpeed = 0.2f;
    public float verticalSpeed = 0.2f;
    public Transform player;

    private Vector3 lastPlayerPosition;

    void Start()
    {
        lastPlayerPosition = player.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = player.position - lastPlayerPosition;
        float parallaxAmountX = deltaMovement.x * horizontalSpeed;
        float parallaxAmountY = deltaMovement.y * verticalSpeed;
        transform.position -= new Vector3(parallaxAmountX, parallaxAmountY, 0);
        lastPlayerPosition = player.position;
    }
}
