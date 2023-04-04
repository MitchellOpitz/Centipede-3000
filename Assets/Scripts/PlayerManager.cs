using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int startingLives = 3;
    public Sprite playerSprite;
    public Vector2 UIposition;
    public GameObject UICanvas;
    public GameObject GameOverCanvas;
    public GameObject player;

    private int currentLives;
    private List<GameObject> lifeIcons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        currentLives = startingLives;
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Remove any existing life icons
        foreach (GameObject icon in lifeIcons)
        {
            Destroy(icon);
        }
        lifeIcons.Clear();

        // Create new life icons
        for (int i = 0; i < currentLives; i++)
        {
            // Create a new GameObject to hold the sprite
            GameObject spriteObject = new GameObject();
            spriteObject.transform.position = new Vector3(UIposition.x + (i * 1), UIposition.y, 0);

            // Add a SpriteRenderer component to the GameObject
            SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();

            // Set the sprite property of the SpriteRenderer
            renderer.sprite = playerSprite;

            // Add the spriteObject to the list of life icons
            lifeIcons.Add(spriteObject);
        }
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateUI();
        if (currentLives <= 0)
        {
            // Player has lost all lives, trigger game over logic
            Destroy(player);
            StartCoroutine(GameOver());
        } else
        {
            // Respawn the player
            StartCoroutine(RespawnPlayer());
        }
    }
    IEnumerator RespawnPlayer()
    {
        // Disable the player's collider and renderer
        player.GetComponent<Collider2D>().enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Move the player to the respawn position
        transform.position = new Vector3(0f, 0f, 0f);

        // Enable the player's collider and renderer
        player.GetComponent<Collider2D>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
    }

    IEnumerator GameOver()
    {
        var score = FindObjectOfType<ScoreManager>().score;
        var highScore = FindObjectOfType<Leaderboard>();
        highScore.GetHighScores();

        yield return new WaitForSeconds(3f);

        UICanvas.SetActive(false);
        GameOverCanvas.SetActive(true);

        var highScorePanel = GameObject.Find("HighScorePanel");
        var retryPanel = GameObject.Find("RetryPanel");
        if (highScore.CheckHighScore(score) && highScore.PersonalBest(score))
        {
            highScorePanel.SetActive(true);
            retryPanel.SetActive(false);
        } else
        {
            highScorePanel.SetActive(false);
            retryPanel.SetActive(true);
        }
    }

    public void ExtraLife()
    {
        currentLives++;
        UpdateUI();
    }

}
