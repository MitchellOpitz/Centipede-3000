using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;
    private List<int> scoresList = new List<int>(new int[10]);
    public GameObject button;

    private string publicLeaderboardKey = "1b1e2bb647b1f858a6ea7822f0f30966ae876ca1c62fffc571144a8122946fbc";

    private void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            for (int i = 0; i < names.Count; i++)
            {
                names[i].text = (i + 1).ToString() + ".) " + msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
            if(GameObject.Find("Loading") != null)
            {
                GameObject.Find("Loading").SetActive(false);
                button.SetActive(true);
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            GetLeaderboard();
        }));
    }

    public void GetHighScores()
    {
        //Debug.Log("Retrieving High Scores.");
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            for (int i = 0; i < 10; i++)
            {
                scoresList[i] = (int)msg[i].Score;
                //Debug.Log(scoresList[i]);
            }
        }));
    }

    public bool CheckHighScore(int myScore)
    {
        for (int i = 0; i < scoresList.Count; i++)
        {
            //Debug.Log("Comparing: " + myScore + " to " + scoresList[i]);
            if (myScore > scoresList[i])
            {
                return true;
            }
        }
        //Debug.Log("No High Score.");
        return false;
    }

    public bool PersonalBest(int myScore)
    {
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);

        if (myScore > bestScore)
        {
            PlayerPrefs.SetInt("HighScore", myScore);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }
}
