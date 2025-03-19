using UnityEngine;

public class AmbientAudioManager : MonoBehaviour
{
    public static AmbientAudioManager Instance { get; private set; }

    [Header("Audio Source")]
    public AudioSource ambientSource;

    [Header("Ambient Sounds")]
    public AudioClip ambientDark;
    public AudioClip ambientTension;
    public AudioClip ambientWhisper;
    public AudioClip ambientWind;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayRandomAmbient();
    }

    private void PlayRandomAmbient()
    {
        AudioClip[] clips = { ambientDark, ambientTension, ambientWhisper, ambientWind };
        if (clips.Length == 0 || ambientSource == null)
        {
            Debug.LogWarning("AmbientAudioManager: No audio clips found or AudioSource is missing!");
            return;
        }

        int randomIndex = Random.Range(0, clips.Length);
        ambientSource.clip = clips[randomIndex];
        ambientSource.loop = true;
        ambientSource.Play();
    }

    // Assign Instance in a static method to resolve UDR0002 warning
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAudioManager()
    {
        if (Instance == null)
        {
            Instance = Object.FindFirstObjectByType<AmbientAudioManager>();
            if (Instance == null)
            {
                Debug.LogWarning("AmbientAudioManager: No AmbientAudioManager instance found in scene.");
            }
        }
    }
}
