using System.Collections;
using UnityEngine;
using TMPro;

public class ScorePopUp : MonoBehaviour
{
    public float duration = 1.5f;
    public float moveSpeed = 1.0f;
    public float fadeSpeed = 1.0f;

    private TextMeshProUGUI scoreText;
    private Vector3 startPosition;
    private float startTime;

    private void Awake()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        startTime = Time.time;
        startPosition = transform.position;
    }

    private void Update()
    {
        // Move the ScorePopUp up over time
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // Fade out the ScorePopUp over time
        float t = (Time.time - startTime) / duration;
        float alpha = Mathf.Lerp(1.0f, 0.0f, t);
        scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, alpha);

        // Destroy the ScorePopUp when it's done fading out
        if (t >= 1.0f)
        {
            Destroy(gameObject);
        }
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, position.y + 0.5f, position.z);
    }
}
