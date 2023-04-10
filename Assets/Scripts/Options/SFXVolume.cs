using UnityEngine;
using UnityEngine.UI;

public class SFXVolume : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        // Load the saved volume level if it exists
        if (PlayerPrefs.HasKey("SFXLevel"))
        {
            slider.value = PlayerPrefs.GetFloat("SFXLevel");
        }
    }

    public void OnSliderValueChanged(float value)
    {
        // Save the volume level
        PlayerPrefs.SetFloat("SFXLevel", value);
        PlayerPrefs.Save();

        if (GameObject.Find("SFXManager"))
        {
            FindObjectOfType<SFXManager>().UpdateVolume();
        }
    }
}
