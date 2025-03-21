using UnityEngine;

public class BackgroundSoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        // Load the ambient_whisper.wav from Resources/Audio
        AudioClip backgroundSound = Resources.Load<AudioClip>("Audio/ambient_wind");

        if (backgroundSound == null)
        {
            Debug.LogError("BackgroundSoundManager: Could not find ambient_whisper.wav in Resources/Audio folder!");
            return;
        }

        audioSource.clip = backgroundSound;
        audioSource.volume = 0.2f; // Minimal volume
        audioSource.loop = true; // Loop the background sound
        audioSource.playOnAwake = true;
        audioSource.spatialBlend = 0f; // Ensure it's a 2D global sound
        audioSource.Play();
    }
}
