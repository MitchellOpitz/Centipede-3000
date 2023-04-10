using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    public int centipedesDestroyed;
    public int spidersDestroyed;
    public int scorpionsDestroyed;
    public int fleasDestroyed;

    public bool resetPrefs;

    public Transform canvas;
    public GameObject textObject;

    private float startTime;

    private void Start()
    {
        if (resetPrefs)
        {
            PlayerPrefs.SetInt("centipedesDestroyed", 0);
            PlayerPrefs.SetInt("spidersDestroyed", 0);
            PlayerPrefs.SetInt("scorpionsDestroyed", 0);
            PlayerPrefs.SetInt("fleasDestroyed", 0);
            PlayerPrefs.SetInt("centipede1", 0);
            PlayerPrefs.SetInt("centipede10", 0);
            PlayerPrefs.SetInt("centipede100", 0);
            PlayerPrefs.SetInt("spider1", 0);
            PlayerPrefs.SetInt("spider10", 0);
            PlayerPrefs.SetInt("spider100", 0);
            PlayerPrefs.SetInt("scorpion1", 0);
            PlayerPrefs.SetInt("scorpion10", 0);
            PlayerPrefs.SetInt("scorpion100", 0);
            PlayerPrefs.SetInt("flea1", 0);
            PlayerPrefs.SetInt("flea10", 0);
            PlayerPrefs.SetInt("flea100", 0);
        }
            centipedesDestroyed = PlayerPrefs.GetInt("centipedesDestroyed");
            spidersDestroyed = PlayerPrefs.GetInt("spidersDestroyed");
            scorpionsDestroyed = PlayerPrefs.GetInt("scorpionsDestroyed");
            fleasDestroyed = PlayerPrefs.GetInt("fleasDestroyed");
    }

    public void UpdateCentipedes()
    {
        centipedesDestroyed++;
        PlayerPrefs.SetInt("centipedesDestroyed", centipedesDestroyed);

        if (centipedesDestroyed == 1)
        {
            PlayerPrefs.SetInt("centipede1", 1);
            TriggerAchievement("Achievement unlocked! First centipede slain!");
        }

        if (centipedesDestroyed == 10)
        {
            PlayerPrefs.SetInt("centipede10", 1);
            TriggerAchievement("Achievement unlocked! 10 centipedes slain!");
        }

        if (centipedesDestroyed == 100)
        {
            PlayerPrefs.SetInt("centipede100", 1);
            TriggerAchievement("Achievement unlocked! 100 centipedes slain!");
        }
    }

    public void UpdateSpiders()
    {
        spidersDestroyed++;
        PlayerPrefs.SetInt("spidersDestroyed", spidersDestroyed);

        if (spidersDestroyed == 1)
        {
            PlayerPrefs.SetInt("spider1", 1);
            TriggerAchievement("Achievement unlocked! First spider slain!");
        }

        if (spidersDestroyed == 10)
        {
            PlayerPrefs.SetInt("spider10", 1);
            TriggerAchievement("Achievement unlocked! 10 spiders slain!");
        }

        if (spidersDestroyed == 100)
        {
            PlayerPrefs.SetInt("spider100", 1);
            TriggerAchievement("Achievement unlocked! 100 spiders slain!");
        }
    }

    public void UpdateScorpions()
    {
        scorpionsDestroyed++;
        PlayerPrefs.SetInt("scorpionsDestroyed", scorpionsDestroyed);

        if (scorpionsDestroyed == 1)
        {
            PlayerPrefs.SetInt("scorpion1", 1);
            TriggerAchievement("Achievement unlocked! First scorpion slain!");
        }

        if (scorpionsDestroyed == 10)
        {
            PlayerPrefs.SetInt("scorpion10", 1);
            TriggerAchievement("Achievement unlocked! 10 scorpions slain!");
        }

        if (scorpionsDestroyed == 100)
        {
            PlayerPrefs.SetInt("scorpion100", 1);
            TriggerAchievement("Achievement unlocked! 100 scorpions slain!");
        }
    }

    public void UpdateFlea()
    {
        fleasDestroyed++;
        PlayerPrefs.SetInt("fleasDestroyed", fleasDestroyed);

        if (fleasDestroyed == 1)
        {
            PlayerPrefs.SetInt("flea1", 1);
            TriggerAchievement("Achievement unlocked! First flea slain!");
        }

        if (fleasDestroyed == 10)
        {
            PlayerPrefs.SetInt("flea10", 1);
            TriggerAchievement("Achievement unlocked! 10 fleas slain!");
        }

        if (fleasDestroyed == 100)
        {
            PlayerPrefs.SetInt("flea100", 1);
            TriggerAchievement("Achievement unlocked! 100 fleas slain!");
        }
    }

    private void TriggerAchievement(string textString)
    {/*
        // Create a new Text object in the middle of the screen
        textObject = new GameObject("TextObject");
        textObject.transform.SetParent(canvas.transform);
        RectTransform rectTransform = textObject.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(40, 10);
        rectTransform.anchoredPosition = Vector2.zero;

        // Add a Text component to the object and set the text
        Text textComponent = textObject.AddComponent<Text>();
        textComponent.text = textString;
        textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textComponent.fontSize = 4;
        textComponent.color = Color.white;
        textComponent.alignment = TextAnchor.MiddleCenter;
        startTime = Time.time;*/
        Debug.Log(textString);
    }

    private void Update()
    {
        /*
        if (textObject != null)
        {
            // Move the ScorePopUp up over time
            transform.position += Vector3.up * 1f * Time.deltaTime;

            // Fade out the ScorePopUp over time
            float t = (Time.time - startTime) / 1.5f;
            float alpha = Mathf.Lerp(1.0f, 0.0f, t);
            textObject.GetComponent<Text>().color = new Color(textObject.GetComponent<Text>().color.r, textObject.GetComponent<Text>().color.g, textObject.GetComponent<Text>().color.b, alpha);

            // Destroy the ScorePopUp when it's done fading out
            if (t >= 1.0f)
            {
                Destroy(gameObject);
            }
        }
        */
    }
}
