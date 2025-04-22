using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SonarLightEffect : MonoBehaviour
{
    public float expandSpeed = 5f;
    public float lifetime = 3f;
    public float detectionRadiusMultiplier = 0.5f;

    private AudioSource audioSource;
    private HashSet<GameObject> hitObjects = new HashSet<GameObject>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Load ping sound from Resources/Audio/ping
        AudioClip pingSound = Resources.Load<AudioClip>("Audio/ping");

        if (pingSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pingSound);
        }
        else
        {
            Debug.LogWarning("Ping sound not found in Resources/Audio/ping or AudioSource missing.");
        }

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;

        float currentRadius = transform.localScale.x * detectionRadiusMultiplier;
        Collider[] hits = Physics.OverlapSphere(transform.position, currentRadius);

        foreach (Collider hit in hits)
        {
            if (!hitObjects.Contains(hit.gameObject))
            {
                SonarInteractable interactable = hit.GetComponent<SonarInteractable>();
                if (interactable != null)
                {
                    interactable.TriggerGlow(3f);
                    hitObjects.Add(hit.gameObject);
                }
            }
        }
    }
}
