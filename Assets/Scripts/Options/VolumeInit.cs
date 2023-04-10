using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeInit : MonoBehaviour
{
    private const string VOLUME_LEVEL_KEY = "VolumeLevel";
    private const float DEFAULT_VOLUME_LEVEL = 0.5f;

    private AudioSource music;

    private void Awake()
    {
        music = GameObject.Find("AudioManager").GetComponent<AudioSource>();

        if (music == null)
        {
            Debug.LogError("AudioManager not found in the scene.");
            return;
        }

        float volumeLevel = PlayerPrefs.GetFloat(VOLUME_LEVEL_KEY, DEFAULT_VOLUME_LEVEL);
        music.volume = volumeLevel;
    }
}
