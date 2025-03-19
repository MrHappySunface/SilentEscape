using UnityEngine;
using UnityEngine.AI;

public class MonsterSoundDetection : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionRadius = 15f;
    public float attentionSpan = 5f;
    public float soundCooldown = 3f;

    private NavMeshAgent agent;
    private bool isAlerted = false;
    private float alertTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isAlerted)
        {
            alertTimer -= Time.deltaTime;
            if (alertTimer <= 0)
            {
                isAlerted = false;
            }
        }
    }

    public void DetectSound(Vector3 soundPosition, float soundIntensity)
    {
        float distanceToSound = Vector3.Distance(transform.position, soundPosition);

        if (distanceToSound <= detectionRadius * soundIntensity && !isAlerted)
        {
            MonsterAI.Instance.AlertMonster(soundPosition);
        }
    }
}
