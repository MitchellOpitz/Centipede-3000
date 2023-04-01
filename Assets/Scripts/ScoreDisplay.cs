using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public int points = 10;
    public GameObject scorePopUpPrefab;
    private Transform scoreCanvas;

    private void Start()
    {
        scoreCanvas = GameObject.Find("ScoreCanvas").transform;
    }

    public void CallScore()
    {
        // Create a new ScorePopUp object at the position of this enemy
        GameObject scorePopUpObject = Instantiate(scorePopUpPrefab, scoreCanvas);

        // Set the score value on the ScorePopUp object
        ScorePopUp scorePopUp = scorePopUpObject.GetComponent<ScorePopUp>();
        scorePopUp.SetScore(points);

        // Set the position of the ScorePopUp object
        scorePopUp.SetPosition(transform.position);
    }

    public void UpdateScore(int newPoints)
    {
        points = newPoints;
    }
}
