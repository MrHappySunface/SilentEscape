using UnityEngine;

public class DoorCreakAudio : MonoBehaviour
{
    public AudioSource doorSource;
    public AudioClip doorCreakClip;

    private bool hasPlayed = false;

    public void PlayDoorCreak()
    {
        if (!hasPlayed)
        {
            doorSource.clip = doorCreakClip;
            doorSource.Play();
            hasPlayed = true;

            Object.FindFirstObjectByType<MonsterSoundDetection>()?.DetectSound(transform.position, 1.2f);

        }
    }
}
