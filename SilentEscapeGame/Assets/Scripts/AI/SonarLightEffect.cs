using UnityEngine;

public class SonarLightEffect : MonoBehaviour
{
    public float expandSpeed = 5f;
    public float lifetime = 3f;
    public AudioClip pingSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (pingSound && audioSource)
            audioSource.PlayOneShot(pingSound);

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;
    }
}
