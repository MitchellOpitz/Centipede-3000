using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundEffect
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(0.1f, 3f)]
        public float pitch = 1f;
        public bool loop;
        public bool playOnAwake;
    }

    public List<SoundEffect> soundEffects;
    private Dictionary<string, AudioSource> audioSources;

    private void Start()
    {
        audioSources = new Dictionary<string, AudioSource>();

        foreach (SoundEffect effect in soundEffects)
        {
            // Create an AudioSource for each sound effect
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = effect.clip;
            source.volume = effect.volume * PlayerPrefs.GetFloat("SFXLevel");
            source.pitch = effect.pitch;
            source.loop = effect.loop;
            source.playOnAwake = effect.playOnAwake;

            // Add it to the dictionary using the effect name as the key
            audioSources[effect.name] = source;
        }
    }

    public void Play(string name)
    {
        // Check if the dictionary contains the named effect
        if (audioSources.TryGetValue(name, out AudioSource source))
        {
            source.Play();
        }
        else
        {
            Debug.LogWarning("No sound effect with name " + name + " found!");
        }
    }

    public void UpdateVolume()
    {
        foreach (SoundEffect effect in soundEffects)
        {
            AudioSource source = audioSources[effect.name];
            source.volume = effect.volume * PlayerPrefs.GetFloat("SFXLevel");
        }
    }
}
