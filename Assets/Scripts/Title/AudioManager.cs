using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip introTrack;
    public AudioClip loopTrack;

    private AudioSource audioSource;
    private bool playingIntro = true;

    private static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = introTrack;
        audioSource.loop = false;
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying && playingIntro)
        {
            playingIntro = false;
            audioSource.clip = loopTrack;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
