using UnityEngine;

public class JumpScareAudio : MonoBehaviour
{
    public AudioSource jumpScareSource;
    public AudioClip jumpScareScream;
    public AudioClip jumpScareViolin;
    public AudioClip jumpScareDoor;

    public void PlayJumpScare()
    {
        AudioClip[] jumpScares = { jumpScareScream, jumpScareViolin, jumpScareDoor };
        int randomIndex = Random.Range(0, jumpScares.Length);
        jumpScareSource.clip = jumpScares[randomIndex];
        jumpScareSource.Play();

        Object.FindFirstObjectByType<MonsterSoundDetection>()?.DetectSound(transform.position, 1.5f);

    }
}
