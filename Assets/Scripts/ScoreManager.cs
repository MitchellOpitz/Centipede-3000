using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;

    private int nextExtraLife;

    private void Start()
    {
        score = 0;
        nextExtraLife = 12000;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        gameOverScoreText.text = "Score: " + score.ToString();
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreText();
        ExtraLifeCheck();
    }

    private void ExtraLifeCheck()
    {
        if(score > nextExtraLife)
        {
            FindObjectOfType<PlayerManager>().ExtraLife();
            nextExtraLife += 12000;
        }
    }
}

