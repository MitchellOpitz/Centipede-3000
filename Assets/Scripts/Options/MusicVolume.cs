using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        // Load the saved volume level if it exists
        if (PlayerPrefs.HasKey("VolumeLevel"))
        {
            slider.value = PlayerPrefs.GetFloat("VolumeLevel");
        }
    }

    public void OnSliderValueChanged(float value)
    {
        if (GameObject.Find("AudioManager"))
        {
            // Find the AudioManager object
            AudioSource music = GameObject.Find("AudioManager").GetComponent<AudioSource>();

            // Set the volume of the audio source to the value of the slider
            music.volume = value;

            // Save the volume level
            PlayerPrefs.SetFloat("VolumeLevel", value);
            PlayerPrefs.Save();
        }
    }
}
