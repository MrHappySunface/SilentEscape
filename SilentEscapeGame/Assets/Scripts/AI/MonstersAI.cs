using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public static MonsterAI Instance { get; private set; }

    [Header("AI Settings")]
    public float patrolRadius = 10f;
    public float patrolSpeed = 2f;
    public Transform[] patrolPoints;

    private NavMeshAgent agent;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ValidateNavMeshPosition();

        InvokeRepeating(nameof(Patrol), 1f, 5f);
        Patrol();
    }

    private void Update()
    {
        if (!agent || !agent.isOnNavMesh) return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Patrol();
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

    // New method to teleport the monster and reset its movement
    public void TeleportMonster(Vector3 teleportDestination)
    {
        if (!agent.isOnNavMesh) return;

        // Stop current movement
        agent.isStopped = true;

        // Check if the teleport position is valid on the NavMesh
        if (NavMesh.SamplePosition(teleportDestination, out NavMeshHit hit, 30f, NavMesh.AllAreas))
        {
            // Move the monster to a valid position on the NavMesh
            transform.position = hit.position;
        }
        else
        {
            // If the position isn't valid, find a nearby valid position
            Debug.LogWarning("Teleport position invalid, using fallback.");
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hitFallback, 30f, NavMesh.AllAreas))
            {
                transform.position = hitFallback.position;
            }
            else
            {
                Debug.LogError("No valid NavMesh position found after teleport.");
            }
        }

        // Resume NavMesh movement (set a new destination or let it patrol again)
        agent.isStopped = false;
        Patrol();  // Resume patrolling, or set a new specific destination
    }

}
