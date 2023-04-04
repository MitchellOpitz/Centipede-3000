using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;

    public UnityEvent<string, int> submitScoreEvent;
    [SerializeField]
    private TMP_InputField inputName;

    private int nextExtraLife;

    private void Start()
    {
        score = 0;
        nextExtraLife = 12000;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
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

    public void SubmitScore()
    {
        submitScoreEvent.Invoke(inputName.text, int.Parse(score.ToString()));
        SceneManager.LoadScene("HighScores");
    }
}

