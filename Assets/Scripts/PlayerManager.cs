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
    public int wavesCleared;
    public Vector3 playerSpawnPosition;

    private int currentLives;
    private List<GameObject> lifeIcons = new List<GameObject>();

    private bool isInvulnerable = false;
    private float invulnerabilityTime = 3f;
    private float invulnerabilityFlashRate = 0.1f;
    private Coroutine flashingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        currentLives = startingLives;
        wavesCleared = 0;
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
        player.GetComponent<PlayerShoot>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Move the player to the respawn position
        player.transform.position = playerSpawnPosition;
        wavesCleared = 0;
        FindObjectOfType<FleaSpawner>().ResetThreshold();

        // Enable the player's collider and renderer
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<PlayerShoot>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;

        isInvulnerable = true;
        flashingCoroutine = StartCoroutine(FlashSprite());

        // Wait for the invulnerability time
        yield return new WaitForSeconds(invulnerabilityTime);

        // Disable invulnerability and stop flashing the player sprite
        isInvulnerable = false;
        if (flashingCoroutine != null)
        {
            StopCoroutine(flashingCoroutine);
            flashingCoroutine = null;
            player.GetComponent<Collider2D>().enabled = true;
            if (player.GetComponent<SpriteRenderer>().enabled == false)
            {
                player.GetComponent<SpriteRenderer>().enabled = true;
                player.GetComponent<PlayerMovement>().hasBeenHit = false;
            }
        }
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

    public float GetMultiplier()
    {
        var multiplier = 1 + (wavesCleared * 0.05f);
        if (multiplier > 2)
        {
            multiplier = 2;
        }
        return multiplier;
    }
    IEnumerator FlashSprite()
    {
        SpriteRenderer playerSpriteRenderer = player.GetComponent<SpriteRenderer>();

        // Loop until invulnerability ends
        while (isInvulnerable)
        {
            // Toggle the player sprite on and off
            playerSpriteRenderer.enabled = !playerSpriteRenderer.enabled;

            // Wait for the specified flashing rate
            yield return new WaitForSeconds(invulnerabilityFlashRate);
        }

        // Ensure the player sprite is visible when invulnerability ends
        playerSpriteRenderer.enabled = true;

        // Stop the flashing coroutine
        flashingCoroutine = null;
    }

}
