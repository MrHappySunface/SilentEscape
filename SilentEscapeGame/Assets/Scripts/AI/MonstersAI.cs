using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public static MonsterAI Instance { get; private set; }

    [Header("AI Settings")]
    public float patrolRadius = 10f;
    public float patrolSpeed = 2f;
    public Transform[] patrolPoints;

    [Header("Audio")]
    public AudioClip breathingSound;

    private NavMeshAgent agent;
    private AudioSource audioSource;
    private bool isBreathing = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        if (breathingSound == null)
        {
            breathingSound = Resources.Load<AudioClip>("Audio/monster_breathing");
            if (breathingSound == null)
            {
                Debug.LogError("MonsterAI: Breathing sound not found in Resources/Audio!");
            }
        }

        if (audioSource != null && breathingSound != null)
        {
            audioSource.clip = breathingSound;
            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }

        ValidateNavMeshPosition();

        InvokeRepeating("Patrol", 1f, 5f);
        Patrol();
    }

    private void Update()
    {
        if (!agent || !agent.isOnNavMesh) return;

        if (agent.velocity.magnitude > 0.1f)
        {
            PlayBreathing();
        }
        else
        {
            StopBreathing();
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Patrol();
        }
    }

    private void PlayBreathing()
    {
        if (!isBreathing && audioSource != null && breathingSound != null)
        {
            audioSource.Play();
            isBreathing = true;
        }
    }

    private void StopBreathing()
    {
        if (isBreathing && audioSource != null)
        {
            audioSource.Stop();
            isBreathing = false;
        }
    }

    public void AlertMonster(Vector3 soundPosition)
    {
        Debug.Log("Monster heard sound at: " + soundPosition);

        if (!agent.isOnNavMesh) return;

        agent.speed = patrolSpeed;
        agent.SetDestination(soundPosition);
    }

    private void Patrol()
    {
        if (!agent.isOnNavMesh) return;

        if (patrolPoints.Length > 0)
        {
            int i = Random.Range(0, patrolPoints.Length);
            agent.speed = patrolSpeed;
            agent.SetDestination(patrolPoints[i].position);
        }
        else
        {
            Vector3 point = transform.position + Random.insideUnitSphere * patrolRadius;
            point.y = transform.position.y;

            if (NavMesh.SamplePosition(point, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
            {
                agent.speed = patrolSpeed;
                agent.SetDestination(hit.position);
            }
        }
    }

    private void ValidateNavMeshPosition()
    {
        if (agent.isOnNavMesh) return;

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 50f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
            transform.position = hit.position;
            Debug.Log($"MonsterAI repositioned to: {hit.position}");
        }
        else
        {
            Debug.LogError("MonsterAI: Could not find valid NavMesh position nearby!");
        }
    }
}
