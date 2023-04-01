using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.2f;
    public float shakeAmount = 0.1f;
    public float decreaseFactor = 1.0f;

    private Vector3 originalPosition;
    private float currentShakeDuration = 0.0f;

    private void Awake()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (currentShakeDuration > 0)
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;

            currentShakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            currentShakeDuration = 0.0f;
            transform.localPosition = originalPosition;
        }
    }

    public void Shake()
    {
        currentShakeDuration = shakeDuration;
    }
}
