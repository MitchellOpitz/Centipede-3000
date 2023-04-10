using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public Slider musicVolumeSlider;
    public AudioSource sfxAudioSource;
    public Slider sfxVolumeSlider;

    private void Start()
    {
        // Set the slider values to the current audio volumes
        musicVolumeSlider.value = musicAudioSource.volume;
        sfxVolumeSlider.value = sfxAudioSource.volume;
    }

    public void OnMusicVolumeChanged(float value)
    {
        musicAudioSource.volume = value;
    }

    public void OnSFXVolumeChanged(float value)
    {
        sfxAudioSource.volume = value;
    }

    public void OnRemapKeyButtonClicked()
    {
        // Handle remapping of keys
    }
}