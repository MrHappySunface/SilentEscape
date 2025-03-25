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
    private float cooldownTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isAlerted)
        {
            alertTimer -= Time.deltaTime;
            if (alertTimer <= 0) isAlerted = false;
        }

        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
    }

    public void DetectSound(Vector3 soundPosition, float soundIntensity)
    {
        if (cooldownTimer > 0 || isAlerted) return;

        float distanceToSound = Vector3.Distance(transform.position, soundPosition);

        if (distanceToSound <= detectionRadius * soundIntensity)
        {
            if (MonsterAI.Instance != null)
            {
                MonsterAI.Instance.AlertMonster(soundPosition);
                isAlerted = true;
                alertTimer = attentionSpan;
                cooldownTimer = soundCooldown;
                Debug.Log("Monster detected a sound at " + soundPosition);
            }
            else
            {
                Debug.LogError("MonsterAI Instance is NULL! Ensure MonsterAI is in the scene.");
            }
        }
    }
}
