using UnityEngine;

public class FootstepAudioManager : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioClip footstepWood;
    public AudioClip footstepMetal;
    public AudioClip footstepConcrete;

    private CharacterController player;

    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (player.isGrounded && player.velocity.magnitude > 0.1f && !footstepSource.isPlaying)
        {
            PlayFootstep();
        }
    }

    void PlayFootstep()
    {
        footstepSource.Play();
        Object.FindFirstObjectByType<MonsterSoundDetection>()?.DetectSound(transform.position, 0.8f);

    }
}
