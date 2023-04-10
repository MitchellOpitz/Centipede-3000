using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementLookup : MonoBehaviour
{
    public string keyName;
    private int keyValue;
    public string achievementText;

    public TMP_Text textField;

    // Start is called before the first frame update
    void Start()
    {
        keyValue = PlayerPrefs.GetInt(keyName);

        if(keyValue == 1)
        {
            textField.text = achievementText;
        }
    }
}
